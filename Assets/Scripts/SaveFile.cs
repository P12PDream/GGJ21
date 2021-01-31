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
    public float attackDamage = 0;
    public float coldResistance = 0;

    public int statPoints = 0;

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

            loadedSave.statPoints = data.statPoints;
            loadedSave.maxHealth = data.maxHealth;
            loadedSave.movementSpeed = data.movementSpeed;
            loadedSave.attackDamage = data.attackDamage;
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

        data.statPoints = loadedSave.statPoints;
        data.maxHealth = loadedSave.maxHealth;
        data.movementSpeed = loadedSave.movementSpeed;
        data.attackDamage = loadedSave.attackDamage;
        data.coldResistance = loadedSave.coldResistance;
        data.currentMaxCombo = loadedSave.currentMaxCombo;

        bf.Serialize(file, data);
        file.Close();
    }

    public void AddProgress(SaveData save)
    {
        loadedSave.statPoints += save.statPoints;
        loadedSave.maxHealth += save.maxHealth;
        loadedSave.movementSpeed += save.movementSpeed;
        loadedSave.attackDamage += save.attackDamage;
        loadedSave.coldResistance += save.coldResistance;
        SaveStats();
    }

}
