using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLimit = 5;
    public float coldDmgPerTick = 5;
    private float currTime;
    public float coldTemperature;
    public GameObject[] extractPoints;


    public float extractionTime;

    public float timer = 0;

    public GameObject player;

    private SaveFile saveFile;


    public Image boboFace;

    public Sprite boboSly;
    public Sprite boboHurt;
    public Sprite boboAngry;
    public Sprite boboHurtBadly;
    public Sprite boboNormal;

    public Text healthText;

    public SaveData thisSessionProgress;

    public bool isExtracting = false;

    private Stats stats;
    private PlayerController pController;

    public Text extractionTest;
    public GameObject extractionPanel;

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
        boboFace.sprite = boboNormal;
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
        healthText.text = Mathf.RoundToInt(stats.health).ToString();
        currTime += 1 * Time.deltaTime;
        if(currTime > timeLimit)
        {
            stats.health -= (coldDmgPerTick - saveFile.loadedSave.coldResistance); //needs to be tweaked to a more cooler variation thing
        }

        if(stats.health < 50 && stats.health > 31)
        {
            boboFace.sprite = boboHurt;
        }

        if(stats.health < 30)
        {
            boboFace.sprite = boboHurtBadly;
        }
        if(stats.health > 50)
        {
            boboFace.sprite = boboNormal;
        }

        if (isExtracting)
        {
            extractionPanel.SetActive(true);
            extractionTest.text = "Extraction in " + (5f - timer).ToString("F1");
            timer += 1 * Time.deltaTime;
            print("extracting .. " + timer.ToString());
            if(timer >= extractionTime && isExtracting)
            {
                extractionPanel.SetActive(false);
                StartCoroutine(ExtractSuccess());
            }
        } else
        {
            if(extractionPanel != null)
                extractionPanel.SetActive(false);
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
