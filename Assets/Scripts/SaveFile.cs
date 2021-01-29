using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Reflection;

[Serializable]
public class SaveData {
    public float maxHealth = 100;
    public float movementSpeed = 5;
    public float eatSpeed = 5;
    public float attackDamage = 10;
    public float attackSpeed = 5;
    public float coldResistance = 5;

    public int mapsFinished = 0;
    public int statPoints = 0;
    public int enemiesKilled = 0;
    public int totalDeaths = 0;

    public int currentMaxCombo = 1;
}

public class SaveFile : MonoBehaviour
{

    public string saveFileName = "bigfoot.bigfoot";
    public int testVar;

    [SerializeField]
    public SaveData loadedSave;
    // Start is called before the first frame update
    void Awake()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        if (File.Exists(saveFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFileName, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);

            loadedSave.mapsFinished = data.mapsFinished;
            loadedSave.enemiesKilled = data.enemiesKilled;
            loadedSave.statPoints = data.statPoints;
            loadedSave.totalDeaths = data.totalDeaths;
            loadedSave.maxHealth = data.maxHealth;
            loadedSave.movementSpeed = data.movementSpeed;
            loadedSave.eatSpeed = data.eatSpeed;
            loadedSave.attackDamage = data.attackDamage;
            loadedSave.attackSpeed = data.attackSpeed;
            loadedSave.coldResistance = data.coldResistance;
            loadedSave.currentMaxCombo = data.currentMaxCombo;
            file.Close();
        } else
        {
            NewSave();
        }
    }

    void Update()
    {
        loadedSave.mapsFinished += 1;
        testVar = loadedSave.mapsFinished;
    }

    public void NewSave()
    {
        loadedSave = new SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    public void SaveStats()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFileName);
        SaveData data = new SaveData();

        data.mapsFinished = loadedSave.mapsFinished;
        data.enemiesKilled = loadedSave.enemiesKilled;
        data.statPoints = loadedSave.statPoints;
        data.totalDeaths = loadedSave.totalDeaths;
        data.maxHealth = loadedSave.maxHealth;
        data.movementSpeed = loadedSave.movementSpeed;
        data.eatSpeed = loadedSave.eatSpeed;
        data.attackDamage = loadedSave.attackDamage;
        data.attackSpeed = loadedSave.attackSpeed;
        data.coldResistance = loadedSave.coldResistance;
        data.currentMaxCombo = loadedSave.currentMaxCombo;

        bf.Serialize(file, data);
        file.Close();
    }
}
