using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    public float flickerInterval;
    private float timer = 0;
    public float flickerMin;
    public float flickerMax;
    private Light fireLight;
    // Start is called before the first frame update
    void Start()
    {
        fireLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.1f;
        if(timer > flickerInterval)
        {
            fireLight.range = Random.Range(flickerMin, flickerMax);
            timer = 0;
        }
    }
}
