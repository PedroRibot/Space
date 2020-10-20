using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBeh : MonoBehaviour
{
    public GameObject turret;
    public GameObject spawnPoint;

    public GameObject nave;

    public GameObject missilePrefab;

    public float tiempoEntreDisparos = 10;
    float timer = 3;




    void Update(){


        if (Vector3.Distance(nave.transform.position, turret.transform.position) <= 150 ){
            
            turret.transform.LookAt(nave.transform.position);

            timer -= Time.deltaTime;

            if (timer <= 0){
                // Disparar
                
                for (int i = 0; i < 3; i++)
                {
                    GameObject m = Instantiate (missilePrefab, spawnPoint.transform.position + Vector3.right * i, turret.transform.rotation);
                    m.GetComponent<MissileBeh>().Homing(nave.gameObject);
                }
               

                timer = tiempoEntreDisparos;
            }


        }

        

    }


}
