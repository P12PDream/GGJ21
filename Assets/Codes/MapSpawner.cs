using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] areaMarkers;
    public float forestDensity = 0.5f;

    public GameObject treePrefab;

    public List<GameObject> campPrefabs = new List<GameObject>();
    public GameObject fire;
    public float campDensity = 0.3f;

    public Enemy meleeEnemyPrefab;
    public Enemy rangedEnemyPrefab;
    public int squadSize;
    public float enemyDensity;

    private void Start()
    {
        areaMarkers = GameObject.FindGameObjectsWithTag("FSM");
        GenerateForests();
        GenerateCamps();
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
        //forest size
        Vector3 forestAreaStart = aMarker.transform.position - new Vector3(size, 0, size);
        Vector3 forestAreaEnd = aMarker.transform.position + new Vector3(size, 0, size);

        float treeAmount = Random.Range(25, 60);

        for(int i = 0; i < treeAmount; i++)
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.position = new Vector3(Random.Range(forestAreaStart.x, forestAreaEnd.x), 0,
                                                  Random.Range(forestAreaStart.z, forestAreaEnd.z));

            tree.transform.localScale = new Vector3(tree.transform.localScale.x * Random.Range(0.7f, 1),
                                                    tree.transform.localScale.y * Random.Range(0.5f, 1.5f),
                                                    tree.transform.localScale.z * Random.Range(0.7f, 1));
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
        campBuildings.transform.Rotate(0, Random.Range(0, 180), 0);

        SpawnSquad(true, buildingAmount + 2, aMarker.transform.position);

        //spawn enemies that live in the camp
        //set this camp as their home camp
    }

    public void SpawnEnemy(Enemy eToSpawn)
    {

    }

    public void SpawnSquad(bool stationary, int amount, Vector3 pos)
    {
        int gunnerAmount = amount / 2;

        for(int i = 0; i < amount; i++)
        {
            if (i < gunnerAmount)
            {
                GameObject re = Instantiate(rangedEnemyPrefab.gameObject, new Vector3(pos.x + Random.Range(-10, 10), pos.y, pos.z + Random.Range(-10, 10)), Quaternion.identity);
                Enemy rangedEnemy = re.GetComponent<Enemy>();
                //set stationary, but turn on intervals?
            }
            else if(i < amount)
            {
                GameObject me = Instantiate(meleeEnemyPrefab.gameObject, new Vector3(pos.x + Random.Range(-10, 10), pos.y, pos.z + Random.Range(-10, 10)), Quaternion.identity);
                Enemy melee�nemy = me.GetComponent<Enemy>();
                //set stuff
            }
                
        }
    }

}