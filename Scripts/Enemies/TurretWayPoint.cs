using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWayPoint : MonoBehaviour
{
    private GameObject player3D;
    public GameObject wayPoint;
    private int currentWayPoint;
    private float speedTurret;
    private Vector3 posEnd;
    private Vector3 posEndTurret;
    private float distanceToPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;

    public GameObject missileTurret;
    private GameObject missileTurretClone;
    public GameObject creatorMissileTurret;
    private float forceMissileTurret;
    private float maxRange;
    private float minRange;

    private bool isHitting;

    public GameObject turretShootSound;

    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        //wayPoint = (GameObject)GameObject.Find("WayPointsYellowTurret");
    }
    void Start()
    {
        currentWayPoint = 0;
        speedTurret = 3.0f;
        speedRotation = 10.0f;
        InvokeRepeating("CreateMissile", 1.0f, 6.0f);
        forceMissileTurret = 20.0f;
        maxRange = 15.0f;
        minRange = 4.0f;
        isHitting = false;
    }

    public void CreateMissile()
    {
        if (General.currentEnemies != 5)
        {
            if (!isHitting)
            {
                if (distanceToPlayer3D > 0.0f && distanceToPlayer3D < maxRange)
                {
                    turretShootSound.gameObject.GetComponent<AudioSource>().Play();
                    missileTurretClone = (GameObject)Instantiate(missileTurret, creatorMissileTurret.gameObject.GetComponent<Transform>().position, creatorMissileTurret.gameObject.GetComponent<Transform>().rotation);
                    missileTurretClone.gameObject.GetComponent<Rigidbody>().velocity = creatorMissileTurret.gameObject.GetComponent<Transform>().forward * forceMissileTurret;
                    Destroy(missileTurretClone.gameObject, 3.0f);
                }
            }
        }
    }

    void Update()
    {
        posEnd = new Vector3(wayPoint.gameObject.GetComponent<Transform>().GetChild(currentWayPoint).gameObject.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, wayPoint.gameObject.GetComponent<Transform>().GetChild(currentWayPoint).gameObject.GetComponent<Transform>().position.z);
        distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, player3D.gameObject.GetComponent<Transform>().position);

        if (!isHitting)
        {
            if (distanceToPlayer3D > 0.0f && distanceToPlayer3D < maxRange)
            {
                posEndTurret = new Vector3(player3D.gameObject.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.gameObject.GetComponent<Transform>().position.z);

                newRotation = Quaternion.LookRotation(player3D.gameObject.GetComponent<Transform>().position - this.gameObject.GetComponent<Transform>().position);
                this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);

                this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posEndTurret, speedTurret * Time.deltaTime);


                if (distanceToPlayer3D < minRange)
                {
                    speedTurret = 0;
                }
                else
                {
                    speedTurret = 1.0f;
                }

            }

            else
            {
                posEndTurret = new Vector3(posEnd.x, this.gameObject.GetComponent<Transform>().position.y, posEnd.z);

                newRotation = Quaternion.LookRotation(posEndTurret - this.gameObject.GetComponent<Transform>().position);
                this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);
                //this.gameObject.GetComponent<Transform>().LookAt(posEnd);

                this.gameObject.GetComponent<Transform>().LookAt(posEnd);
                this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posEnd, speedTurret * Time.deltaTime);
                if (this.gameObject.GetComponent<Transform>().position == posEnd)
                {
                    currentWayPoint++;
                    if (currentWayPoint >= wayPoint.gameObject.GetComponent<Transform>().childCount)
                    {
                        currentWayPoint = 0;
                    }
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