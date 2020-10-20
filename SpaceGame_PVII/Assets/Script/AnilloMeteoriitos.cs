using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnilloMeteoriitos : MonoBehaviour
{

    
    public GameObject[] meteoritePrefab;
    public GameObject planet;

    public float radioSpawneo = 400;

    public float numeroAsteroides = 200;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numeroAsteroides; i++)
        {
            GameObject temp = Instantiate(meteoritePrefab[Random.Range(0,9)], Random.onUnitSphere * radioSpawneo, Quaternion.identity);
            temp.transform.localScale = temp.transform.localScale * Random.Range(0.5f, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
