using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;
    public float size;
    public Camera snowCam;

    public GameObject[] tilesetPrefabs;

    public int rowCount = 5;
    public int columnCount = 5;
    public float columnLength = 5;

    public GameObject treePrefab;

    public GameObject smallForest, mediumForest, bigForest;
    public GameObject stone1, stone2, stone3;

    public GameObject camp;


    public void Start()
    {
        //GenerateMap(rowCount, columnCount);
        GenerateMap(size);
    }

    public void GenerateMap(float width)
    {
        map.transform.localScale = new Vector3(width, 1, width);
        snowCam.orthographicSize = width/2;
    }

    public GameObject GetTile(int id)
    {
        if (tilesetPrefabs.Length >= id)
            return tilesetPrefabs[id];
        else
            return null;
    }

    public void GenerateMap(int row, int column)
    {
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject go = Instantiate(GetTile(Random.Range(0, tilesetPrefabs.Length)));
                go.transform.position = pos + new Vector3(0, 0, columnLength);
                pos = go.transform.position;
                //SpawnPropsOnTile(go);
            }

            pos = new Vector3(pos.x + columnLength, 0, 0);
        }
    }

    public void SpawnPropsOnTile(GameObject tile, int spotAmount, bool hasCamp)
    {
        bool campSpawned = false;
        if(!hasCamp)
            campSpawned = true;

        /*
         * 0 = nothing
         * 1 = nothing
         * 2 = nothing
         * 3 = smallForest
         * 4 = medium forest
         * 5 = camp (if no camp, nothing)
         * 6 = enemySquad
         * 7 = enemyScout(s)
         * 8 = 
         * 9 = 
         */


        for(int i = 0; i < spotAmount; i++)
        {
            
        }


    }
}
