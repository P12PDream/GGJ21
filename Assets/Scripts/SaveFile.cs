using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Reflection;

[Serializable]
public class SaveData {
    public float maxHealth = 0;
    public float movementSpeed = 0;
    public float eatSpeed = 0;
    public float attackDamage = 0;
    public float attackSpeed = 0;
    public float coldResistance = 0;

    public int mapsFinished = 0;
    public int statPoints = 0;
    public int enemiesKilled = 0;
    public int totalDeaths = 0;

    public int currentMaxCombo = 1;
}

public class SaveFile : MonoBehaviour
{

    public string saveFileName = "bigfoot.bigfoot";

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

    public void NewSave()
    {
        loadedSave = new SaveData();
        loadedSave.movementSpeed = 5;
        loadedSave.maxHealth = 100;
        //add other default/starting stats here
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

    public void AddProgress(SaveData save)
    {
        loadedSave.mapsFinished += save.mapsFinished;
        loadedSave.enemiesKilled += save.enemiesKilled;
        loadedSave.statPoints += save.statPoints;
        loadedSave.totalDeaths += save.totalDeaths;
        loadedSave.maxHealth += save.maxHealth;
        loadedSave.movementSpeed += save.movementSpeed;
        loadedSave.eatSpeed += save.eatSpeed;
        loadedSave.attackDamage += save.attackDamage;
        loadedSave.attackSpeed += save.attackSpeed;
        loadedSave.coldResistance += save.coldResistance;
        loadedSave.currentMaxCombo += save.currentMaxCombo;
        SaveStats();
    }

}
