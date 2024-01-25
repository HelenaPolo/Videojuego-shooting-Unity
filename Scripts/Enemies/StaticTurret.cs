using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurret : MonoBehaviour
{
    private GameObject player3D;
    private float distanceToPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;
    private float speedRotation2;

    public GameObject missileStaticTurret;
    private GameObject missileStaticTurretClone;
    public GameObject creatorMissileTurret;
    private float forceMissileTurret;

    private float minRage;

    public GameObject turretShootSound;

    private float speedTurret;
    private bool isHitting;


    public void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
    }

    // Start is called before the first frame update
    void Start()
    {
        speedRotation = 1.0f;
        speedRotation2 = 8.0f;
        InvokeRepeating("CreateMissile", 1.0f, 10.0f);
        forceMissileTurret = 50.0f;
        minRage = 10.0f;
        speedTurret = 3.0f;
        isHitting = false;

    }

    public void CreateMissile()
    {
        if (General.currentEnemies != 5)
        {
            if (!isHitting)
            {
                if (distanceToPlayer3D < 80.0f)
                {
                    turretShootSound.gameObject.GetComponent<AudioSource>().Play();
                    missileStaticTurretClone = (GameObject)Instantiate(missileStaticTurret, creatorMissileTurret.gameObject.GetComponent<Transform>().position, creatorMissileTurret.gameObject.GetComponent<Transform>().rotation);
                    missileStaticTurretClone.gameObject.GetComponent<Rigidbody>().velocity = creatorMissileTurret.gameObject.GetComponent<Transform>().forward * forceMissileTurret;
                    Destroy(missileStaticTurretClone.gameObject, 3.0f);

                }
            }
        }
    }

    void Update()
    {
        distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, player3D.gameObject.GetComponent<Transform>().position);
        //Debug.Log(distanceToPlayer3D);
        if (!isHitting)
        {
            if (distanceToPlayer3D < 80.0f)
            {
                newRotation = Quaternion.LookRotation(player3D.gameObject.GetComponent<Transform>().position - this.gameObject.GetComponent<Transform>().position);
                this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);
                if (distanceToPlayer3D < minRage)
                {
                    //this.gameObject.GetComponent<Transform>().position
                }

            }
            else
            {
                this.gameObject.GetComponent<Transform>().Rotate(0.0f, speedRotation2 * Time.deltaTime, 0.0f);
                //Debug.Log(this.gameObject.GetComponent<Transform>().localEulerAngles.y);
                if (this.gameObject.GetComponent<Transform>().localEulerAngles.y > 80.0f)
                {
                    speedRotation2 = -8.0f;
                    this.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, 80.0f, 0.0f);
                }
                else if (this.gameObject.GetComponent<Transform>().localEulerAngles.y < 1.0f)
                {
                    speedRotation2 = 8.0f;
                    this.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0.0f, 1.0f, 0.0f);
                }
            }
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PetalBalls" || other.gameObject.tag == "RainbowBalls")
        {
            Destroy(other.gameObject);
            speedTurret = 0;
            isHitting = true;

            Invoke("MoveAgain", 5.0f);

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "BubbleBalls")
        {
            Destroy(other.gameObject);
            speedTurret = 0;
            isHitting = true;

            Invoke("MoveAgain", 5.0f);

        }
    }

    private void MoveAgain()
    {
        isHitting = false;
        speedTurret = 3.0f;
    }

}



