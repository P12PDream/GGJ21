using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] areaMarkers;
    public float forestDensity = 0.5f;

    public List<GameObject> treePrefabs = new List<GameObject>();
    public List<GameObject> propPrefabs = new List<GameObject>();

    public List<GameObject> campPrefabs = new List<GameObject>();
    public GameObject fire;
    public float campDensity = 0.3f;

    public Enemy meleeEnemyPrefab;
    public Enemy rangedEnemyPrefab;
    public Enemy scoutEnemyPrefab;
    public GameObject pickupPrefab;

    public int squadSize;
    public float enemyDensity;

    private void Start()
    {
        areaMarkers = GameObject.FindGameObjectsWithTag("FSM");
        GenerateForests();
        GenerateCamps();
        GenerateProps();
    }

    public void GenerateProps()
    {
        //float forestAmount = areaMarkers.Length * forestDensity;
        //forestAmount = (int)forestAmount;
        
        for (int i = 0; i < areaMarkers.Length; i++)
        {
            SpawnPropsAtAreaMarker(areaMarkers[i], Random.Range(30, 40));
        }
    }

    public void GenerateForests()
    {
        float forestAmount = areaMarkers.Length * forestDensity;
        forestAmount = (int)forestAmount;

        List<bool> hasForest = new List<bool>();
        for (int i = 0; i < areaMarkers.Length; i++)
        {
            if (i <= forestAmount)
                hasForest.Add(true);
            else
                hasForest.Add(false);
        }

        ListHelper.Shuffle(hasForest);

        for(int i = 0; i < areaMarkers.Length; i++)
        {
            //spawn forest on this area marker
            if (hasForest[i])
                SpawnForestAtAreaMarker(areaMarkers[i], Random.Range(15,30));
        }
    }

    public void SpawnForestAtAreaMarker(GameObject aMarker, float size)
    {
        int forestType = Random.Range(0, treePrefabs.Count);

        //forest size
        Vector3 forestAreaStart = aMarker.transform.position - new Vector3(size, 0, size);
        Vector3 forestAreaEnd = aMarker.transform.position + new Vector3(size, 0, size);

        float treeAmount = Random.Range(25, 60);

        for(int i = 0; i < treeAmount; i++)
        {
            GameObject tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Count)]);
            tree.transform.position = new Vector3(Random.Range(forestAreaStart.x, forestAreaEnd.x), 0,
                                                  Random.Range(forestAreaStart.z, forestAreaEnd.z));

            tree.transform.localScale = new Vector3(tree.transform.localScale.x * Random.Range(0.2f, .5f),
                                                    tree.transform.localScale.y * Random.Range(0.5f, 1.5f),
                                                    tree.transform.localScale.z * Random.Range(0.2f, .5f));

            tree.transform.Rotate(0, Random.Range(0, 360), 0);
        }
    }

    public void SpawnPropsAtAreaMarker(GameObject aMarker, float size)
    {
        Vector3 forestAreaStart = aMarker.transform.position - new Vector3(size, 0, size);
        Vector3 forestAreaEnd = aMarker.transform.position + new Vector3(size, 0, size);

        float propAmount = Random.Range(5, 25);

        for (int i = 0; i < propAmount; i++)
        {
            GameObject prop = Instantiate(propPrefabs[Random.Range(0, propPrefabs.Count)]);
            prop.transform.position = new Vector3(Random.Range(forestAreaStart.x, forestAreaEnd.x), 0,
                                                  Random.Range(forestAreaStart.z, forestAreaEnd.z));

            prop.transform.localScale = new Vector3(prop.transform.localScale.x * Random.Range(0.6f, 1.2f),
                                                    prop.transform.localScale.y * Random.Range(0.6f, 1.2f),
                                                    prop.transform.localScale.z * Random.Range(0.6f, 1.2f));

            prop.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }


    public void GenerateCamps()
    {
        float campAmount = areaMarkers.Length * campDensity;
        campAmount = (int)campAmount;

        List<bool> hasCamp = new List<bool>();
        for (int i = 0; i < areaMarkers.Length; i++)
        {
            if (i <= campAmount)
                hasCamp.Add(true);
            else
                hasCamp.Add(false);
        }

        ListHelper.Shuffle(hasCamp);

        for (int i = 0; i < areaMarkers.Length; i++)
        {
            //spawn forest on this area marker
            if (hasCamp[i])
                SpawnCamp(areaMarkers[i]);
        }
    }

    public void SpawnCamp(GameObject aMarker)
    {
        int buildingAmount = Random.Range(0, campPrefabs.Count);

        GameObject campfire = Instantiate(fire, aMarker.transform.position, Quaternion.identity);
        GameObject campBuildings = Instantiate(campPrefabs[buildingAmount], campfire.transform.position, Quaternion.identity);
        campBuildings.transform.Rotate(0, Random.Range(0, 360), 0);

        SpawnSquad(true, buildingAmount + 2, campfire);

        SpawnEnemy(scoutEnemyPrefab, campfire.transform.position + new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)), campfire);

        int dropAmount = Random.Range(1, 3);

        for(int i = 0; i < dropAmount; i++)
        {
            GameObject e = Instantiate(pickupPrefab.gameObject, new Vector3(campfire.transform.position.x + Random.Range(-10, 10), 2, campfire.transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
            //do stuff with the pickup?
        }

        //spawn enemies that live in the camp
        //set this camp as their home camp
    }

    public void SpawnEnemy(Enemy eToSpawn, Vector3 pos, GameObject homeCamp)
    {
        GameObject e = Instantiate(eToSpawn.gameObject, new Vector3(pos.x + Random.Range(-10, 10),
                   pos.y, pos.z + Random.Range(-10, 10)), Quaternion.identity);
        Enemy ee = e.GetComponent<Enemy>();
        ee.homeCamp = homeCamp;
    }

    public void SpawnSquad(bool stationary, int amount, GameObject camp)
    {
        int gunnerAmount = amount / 2;

        for(int i = 0; i < amount; i++)
        {
            if (i < gunnerAmount)
            {
                GameObject re = Instantiate(rangedEnemyPrefab.gameObject, new Vector3(camp.transform.position.x + Random.Range(-10, 10), 
                    camp.transform.position.y, camp.transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
                Enemy e = re.GetComponent<Enemy>();
                e.homeCamp = camp;
                //set stationary, but turn on intervals?
            }
            else if(i < amount)
            {
                int r = Random.Range(0, 2);
                GameObject me = Instantiate(((r == 0) ? meleeEnemyPrefab.gameObject : scoutEnemyPrefab.gameObject), new Vector3(camp.transform.position.x + Random.Range(-10, 10), 
                    camp.transform.position.y, camp.transform.position.z + Random.Range(-10, 10)), Quaternion.identity);
                Enemy e = me.GetComponent<Enemy>();
                e.homeCamp = camp;
                //set stuff
            }
                
        }
    }

}
