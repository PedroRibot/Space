using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMissile : MonoBehaviour
{

    public Transform pDisparoNM;

    public Transform pDisparoHM1;
    public Transform pDisparoHM2;

    public GameObject missilePrefab;
    public ParticleSystem disparoEffect;
    
    // Start is called before the first frame update
    void HomingMissile(){

    }

    public void NormalMissile(){

        Instantiate(missilePrefab, pDisparoNM.transform.position + Vector3.right, Camera.main.transform.rotation);
        Instantiate(missilePrefab, pDisparoNM.transform.position + Vector3.left, Camera.main.transform.rotation);
        disparoEffect.Play();
    }
}
