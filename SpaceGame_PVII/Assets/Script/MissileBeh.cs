using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBeh : MonoBehaviour
{
    public float speed = 250;
    public ParticleSystem Expl;
    public float turn = 2;

    bool homing = false;

    GameObject objToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (homing == true){
            
            var rocketTargetRotation = Quaternion.LookRotation(objToFollow.transform.position - transform.position);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turn);

        }

        Destroy(this, 10);

    }

    void OnCollisionEnter(Collision collision)
    {
      Instantiate(Expl, this.transform.position, Quaternion.identity);

      if (collision.transform.tag == "Asteroid" || collision.transform.tag == "Enemy"){
          Destroy(collision.gameObject);
      }else{
          Destroy(this);
      }
      
    }

    public void Homing(GameObject g){
        homing = true;
        objToFollow = g;
        speed = Random.Range(105, 120);
        turn = Random.Range(1, 2);
    }
}
