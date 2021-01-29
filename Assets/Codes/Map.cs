using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] tilesetPrefabs;

    public int rowCount = 5;
    public float columnLength = 5;

    public void Start()
    {
        GenerateMap(rowCount, 10);
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

        for( int i = 0; i < row; i++)
        {
            for(int j = 0; j < column; j++)
            {
                GameObject go = Instantiate(GetTile(Random.Range(0, tilesetPrefabs.Length)));
                go.transform.position = pos + new Vector3(0, 0, columnLength);
                pos = go.transform.position;
            }

            pos = new Vector3(pos.x + columnLength, 0, 0); 
        }
    }

}
