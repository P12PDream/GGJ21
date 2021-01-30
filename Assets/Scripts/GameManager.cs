using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLimit;
    public float coldTemperature;
    public GameObject[] extractPoints;


    public float extractionTime;

    public float timer = 0;

    public GameObject player;

    private SaveFile saveFile;

    public SaveData thisSessionProgress;

    public bool isExtracting = false;

    private Stats stats;
    private PlayerController pController;


    void Awake()
    {
        stats = player.GetComponent<Stats>();
        pController = player.GetComponent<PlayerController>();
        saveFile = GetComponent<SaveFile>();
        saveFile.LoadStats();
        thisSessionProgress = new SaveData();
    }

    void Start()
    {
        LoadStatsToPlayer();
    }

    public void LoadStatsToPlayer()
    {
        stats.maxHealth = saveFile.loadedSave.maxHealth;
        stats.health = saveFile.loadedSave.maxHealth;
        pController.speed = saveFile.loadedSave.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isExtracting)
        {
            timer += 1 * Time.deltaTime;
            print("extracting .. " + timer.ToString());
            if(timer >= extractionTime && isExtracting)
            {
                isExtracting = false;
                StartCoroutine(ExtractSuccess());
            }
        }
    }

    IEnumerator ExtractSuccess()
    {
        yield return new WaitForSeconds(0.5f);
        //fade screen black and go to hideout
        saveFile.AddProgress(thisSessionProgress);
        saveFile.SaveStats();
        SceneManager.LoadScene("Hideout");
    }
}
