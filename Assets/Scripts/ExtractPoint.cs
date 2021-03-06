using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractPoint : MonoBehaviour
{

    public GameObject player;
    public GameObject gManagerObject;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        print("enter");
        if (other.transform.root.GetComponent<PlayerController>())
        {
            print("enter");
            gameManager.isExtracting = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        print("leave");
        if (other.transform.root.GetComponent<PlayerController>())
        {
            gameManager.isExtracting = false;
            gameManager.timer = 0;
        }
    }
}
