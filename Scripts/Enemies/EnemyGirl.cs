using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGirl : MonoBehaviour
{
    private GameObject player3D;
    private float distanceToPlayer3D;
    private Vector3 posPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;
    private float speedMove;
    private bool bloqAttack;
    private int girlLife;
    private bool isDie;
    private bool isHitting;
    private GameObject portal1;
    private Vector3 posPortal1;

    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        portal1 = (GameObject)GameObject.Find("Portal1");

    }

    void Start()
    {
        speedRotation = 5.0f;
        bloqAttack = false;
        speedMove = 3.0f;
        girlLife = 3;
        isDie = false;
        isHitting = false;
        Debug.Log(posPortal1.ToString());
    }

    public void desBloqAttack()
    {
        bloqAttack = false;
        speedMove = 3.0f;

    }

    private void RunAway()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Run");
        this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPortal1, speedMove * Time.deltaTime);
        //Debug.Log(posPortal1.ToString());

    }

    void Update()
    {
        if (!isDie)
        {
            posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);

            distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);
            this.gameObject.GetComponent<Animator>().SetFloat("Distance", distanceToPlayer3D);

            if (distanceToPlayer3D < 15.0f)
            {

                if (distanceToPlayer3D < 10.0f)
                {
                    speedMove = 0;


                    if (!bloqAttack)
                    {
                        this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                        bloqAttack = true;
                        Invoke("desBloqAttack", 3.0f);
                    }
                }
                else
                {
                    if (!isHitting)
                    {
                        this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);
                    }
                }



                newRotation = Quaternion.LookRotation(posPlayer3D - this.gameObject.GetComponent<Transform>().position);
                this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);

            }
        }
        else
        {
            posPortal1 = new Vector3(portal1.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, portal1.GetComponent<Transform>().position.z);
            newRotation = Quaternion.LookRotation(posPortal1 - this.gameObject.GetComponent<Transform>().position);
            this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);

            Invoke("RunAway", 5.0f);

            General.deadEnemies++;

        }

    }




    private void MoveAgain()
    {
        isHitting = false;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PetalBalls" || other.gameObject.tag == "RainbowBalls")
        {
                girlLife--;
                Destroy(other.gameObject);
                if (girlLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    isHitting = true;
                    Invoke("MoveAgain",1.0f);
                }
                else
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                    
                    Invoke("RunAway", 5.0f);
                }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal1")
        {
            Destroy(this.gameObject, 1.0f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!isDie)
        {
            if (other.gameObject.tag == "BubbleBalls")
            {
                girlLife--;
                Destroy(other.gameObject);

                if (girlLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    isHitting = true;
                    Invoke("MoveAgain", 1.0f);
                }
                else
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");

                    Invoke("RunAway", 5.0f);
                }
            }
        }
    }
}






