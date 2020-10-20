using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRecover : MonoBehaviour
{
	public float speed = 100;
	public float rSpeed = 1000;
	public float actualSpeed;
	public float minSpeed = 25;
	public float maxSpeedBoost = 150;
	public float efectoGravedad = 25.0f;
	public float tiempoRecuperarRotacion = 5;


	public float distanciaSensor = 50;
	public float anguloSensibilidadSensor = 160;

	bool boost = false;

	public GameObject Cabina;
	public ParticleSystem BoostPS;

	public List<Ray> rays = new List<Ray>();

	FireMissile fireCMP;

	public float tiempoEntreDisparos = 2;
	float timer ;


	RaycastHit hitInfoF;
	RaycastHit hitInfoRay;
	//RaycastHit hitInfoL;
	//RaycastHit hitInfoU;
	//RaycastHit hitInfoD;

	bool dodge = false;
	void Start()
	{
		actualSpeed = speed;
		fireCMP = this.GetComponent<FireMissile>();
		timer = 0;
	}

    // Update is called once per frame
    void Update()
    {
        //Movimiento de la nave hacia a delante
        transform.position += transform.forward * Time.deltaTime * actualSpeed;

        RotacionCabinaYGiro();
        RecuperacionRotacionCabina();
        Boost();
        RecuperacionBoostANormalidad();
		FireNMissile();






        Ray rayF = new Ray(transform.position, transform.forward);


        Ray rayR = new Ray(transform.position + transform.right * 5, transform.forward);
        Ray rayL = new Ray(transform.position + transform.right * -5, transform.forward);
        Ray rayU = new Ray(transform.position + transform.up * 5, transform.forward);
        Ray rayD = new Ray(transform.position + transform.up * -5, transform.forward);

        rays.Add(rayR);
        rays.Add(rayL);
        rays.Add(rayU);
        rays.Add(rayD);


        if (Physics.Raycast(rayF, out hitInfoF, 50))
        {
            if (Vector3.Angle(transform.forward, hitInfoF.normal) < anguloSensibilidadSensor && dodge == false)
            {
                foreach (var ray in rays)
                {
                    if (CheckRay(ray) == false)
                    {
                        print("Dodge" + (ray.origin - transform.position));
                        dodge = true;
                        break;
                    }
                    else
                    {
                        print("Collision");
                    }
                }
            }
        }
        else
        {
            dodge = false;
        }

        

        /*if (Physics.Raycast(rayL, out hitInfoL, 50))
		{

		}
		if (Physics.Raycast(rayU, out hitInfoU, 50))
		{

		}
		if (Physics.Raycast(rayD, out hitInfoD, 50))
		{

		}*/



        /*if (hitInfoF.distance == 0)
			Debug.DrawRay(transform.position, this.transform.forward * distanciaSensor, Color.green);

		if (hitInfoR.distance == 0)
			Debug.DrawRay(transform.position + transform.right * 5, this.transform.forward * distanciaSensor, Color.green);

		if (hitInfoL.distance == 0)
			Debug.DrawRay(transform.position + transform.right * -5, this.transform.forward * distanciaSensor, Color.green);

		if (hitInfoU.distance == 0)
			Debug.DrawRay(transform.position + transform.up * 5, this.transform.forward * distanciaSensor, Color.green);

		if (hitInfoD.distance == 0)
			Debug.DrawRay(transform.position + transform.up * -5, this.transform.forward * distanciaSensor, Color.green);*/


        Debug.DrawRay(transform.position, this.transform.forward * hitInfoF.distance, Color.yellow);
        /*Debug.DrawRay(transform.position + transform.right * 5, this.transform.forward * hitInfoR.distance, Color.yellow);
		Debug.DrawRay(transform.position + transform.right * -5, this.transform.forward * hitInfoL.distance, Color.yellow);
		Debug.DrawRay(transform.position + transform.up * 5, this.transform.forward * hitInfoU.distance, Color.yellow);
		Debug.DrawRay(transform.position + transform.up * -5, this.transform.forward * hitInfoD.distance, Color.yellow);
		*/
        Debug.DrawRay(hitInfoF.point, hitInfoF.normal * 20, Color.red);


    }

    private void FireNMissile()
    {
        if (Input.GetAxis("LeftTrigger") > 0 && timer <= 0)
        {
            fireCMP.NormalMissile();
            timer = tiempoEntreDisparos;
        }

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private bool CheckRay(Ray ray)
	{
		if (Physics.Raycast(ray, out hitInfoRay, 50))
		{
			return true;
		}
		return false;
	}

	private void RecuperacionBoostANormalidad()
	{
		if (actualSpeed > speed && boost == false)
		{
			actualSpeed = Mathf.Lerp(actualSpeed, speed, Time.deltaTime);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 90, Time.deltaTime);
		}
	}

	private void RecuperacionRotacionCabina()
	{
		if (Input.GetAxis("RightVertical") == 0 && -Input.GetAxis("RightHorizontal") == 0)
		{
			Quaternion rotation = transform.rotation;
			Cabina.transform.rotation = Quaternion.Lerp(Cabina.transform.rotation, rotation, Time.deltaTime * tiempoRecuperarRotacion);
		}
	}

	private void RotacionCabinaYGiro()
	{
		Vector3 v3 = new Vector3(Input.GetAxis("Vertical") * 0.4f, 0.0f, -Input.GetAxis("Horizontal") * 0.6f);
		transform.Rotate(v3 * rSpeed * Time.deltaTime);

		if (boost != true){
			Vector3 v32 = new Vector3(-Input.GetAxis("RightVertical"), 0.0f, -Input.GetAxis("RightHorizontal"));
			Cabina.transform.Rotate(v32 * rSpeed * Time.deltaTime);
		}
		
	}

	void Boost()
	{
		
		if (Input.GetAxis("RightTrigger") > 0)
		{
			if (!BoostPS.isPlaying)
			{
				BoostPS.Play();
			}

			if (transform.rotation == Cabina.transform.rotation)
			{
				actualSpeed = Mathf.Lerp(actualSpeed, maxSpeedBoost, Time.deltaTime *4);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 130, Time.deltaTime * 4);
				boost = true;
			}
			else
			{
				Quaternion rotation = Cabina.transform.rotation;
				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 7);
				actualSpeed = Mathf.Lerp(actualSpeed, maxSpeedBoost, Time.deltaTime * 4);
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 130, Time.deltaTime * 4);
				boost = true;
			}
		}
		else
		{
			boost = false;
			if (BoostPS.isPlaying)
			{
				BoostPS.Stop();
			}
		}
		
	}
}
