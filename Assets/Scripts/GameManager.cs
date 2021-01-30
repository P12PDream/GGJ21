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

    private SaveData thisSessionProgress;

    public bool isExtracting = false;

    private ExtractPoint ePoint;

    void Start()
    {
        saveFile = GetComponent<SaveFile>();
        saveFile.LoadStats();
        thisSessionProgress = new SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        thisSessionProgress.statPoints += 1;
        if (isExtracting)
        {
            timer += 1 * Time.deltaTime;
            print("extracting .. " + timer.ToString());
            if(timer >= extractionTime)
            {
                isExtracting = false;
                StartCoroutine(ExtractSuccess());
            }
        }
    }

    IEnumerator ExtractSuccess()
    {
        saveFile.AddProgress(thisSessionProgress);
        saveFile.SaveStats();
        yield return new WaitForSeconds(0.5f);
        //fade screen black and go to hideout

        SceneManager.LoadScene("Hideout");
    }
}
