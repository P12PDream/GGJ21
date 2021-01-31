using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPointPickup : MonoBehaviour
{

    public GameObject player;
    public GameObject GameManager;
    private GameManager gamemgnragfdsmfdnfs;

    public float sinVal = 0;
    public float cosVal = 0;
    public float sinMultiplier = 0;
    public float sinTime = 0;

    public Light light;

    private bool isPickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        gamemgnragfdsmfdnfs = GameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame1
    void Update()
    {
        sinVal = Mathf.Sin(20 / sinTime) * sinMultiplier;
        transform.Rotate(new Vector3(transform.rotation.x + sinVal, transform.rotation.y + sinVal, transform.rotation.z + sinVal));
    }

    public void collect()
    {
        FindObjectOfType<GameManager>().thisSessionProgress.statPoints += 1;
        isPickedUp = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<PlayerController>() && !isPickedUp)
        {
            collect();
        }
    }
}
