using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] areaMarkers;
    public float forestDensity = 0.5f;

    public GameObject treePrefab;


    private void Start()
    {
        areaMarkers = GameObject.FindGameObjectsWithTag("FSM");
        GenerateForests();
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
}
