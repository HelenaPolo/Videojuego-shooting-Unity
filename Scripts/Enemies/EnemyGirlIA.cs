using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyGirlIA : MonoBehaviour
{
    private GameObject player3D;
    private float distanceToPlayer3D;
    private Vector3 posPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;
    private float speedMove;
    private bool bloqAttack;
    private int girlLife;
    private int minGirlLife;
    private bool isDie;
    private bool isHitting;
    private GameObject portal1;
    private Vector3 posPortal1;
    private GameObject enemiesInventary;
    private GameObject enemyDeadSound;

    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        portal1 = (GameObject)GameObject.Find("Portal1");
        enemiesInventary = (GameObject)GameObject.Find("EnemiesInventary");
        enemyDeadSound = (GameObject)GameObject.Find("EnemyDeadSound");

    }

    void Start()
    {
        speedRotation = 5.0f;
        bloqAttack = false;
        speedMove = 3.0f;
        girlLife = 3;
        minGirlLife = 0;
        isDie = false;
        isHitting = false;
        posPortal1 = new Vector3(portal1.gameObject.GetComponent<Transform>().position.x, portal1.gameObject.GetComponent<Transform>().position.y, portal1.gameObject.GetComponent<Transform>().position.z);
    }

    public void desBloqAttack()
    {
        bloqAttack = false;
        speedMove = 3.0f;

    }

    private void RunAway()
    {
        //this.gameObject.GetComponent<Animator>().SetTrigger("Run");
        this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPortal1, speedMove * Time.deltaTime);
        speedMove = 5.0f;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    void Update()
    {
        if (!isDie)
        {
            posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);

            distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);
            this.gameObject.GetComponent<Animator>().SetFloat("Distance", distanceToPlayer3D);

            if (distanceToPlayer3D < 50.0f)
            {

                if (distanceToPlayer3D < 3.0f)
                {
                    speedMove = 0;


                    if (!bloqAttack)
                    {
                        this.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                        bloqAttack = true;
                        Invoke("desBloqAttack", 1.0f);
                    }
                }
                else
                {
                    if (!isHitting && !isDie)
                    {
                        //this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);
                        this.gameObject.GetComponent<NavMeshAgent>().SetDestination(posPlayer3D);

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
                    


    }


    }

    private void ActiveSound()
    {
        enemyDeadSound.gameObject.GetComponent<AudioSource>().Play();

    }


    private void MoveAgain()
    {
        isHitting = false;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!isDie) {
            if (other.gameObject.tag == "PetalBalls" || other.gameObject.tag == "RainbowBalls")
            {
                girlLife = girlLife - 1;
                Destroy(other.gameObject);
                this.gameObject.GetComponent<AudioSource>().Play();

                if (girlLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    isHitting = true;
                    Invoke("MoveAgain", 0.5f);
                }
                else if (girlLife == 0)
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                    Invoke("ActiveSound", 1.0f);

                    girlLife = minGirlLife;

                    Invoke("RunAway", 5.0f);
                    General.isEnemyDead = true;

                    if (minGirlLife == 0)
                    {
                        General.currentEnemies = General.currentEnemies + 1;
                        enemiesInventary.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentEnemies.ToString();

                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MessagePortal")
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
                this.gameObject.GetComponent<AudioSource>().Play();


                if (girlLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    isHitting = true;
                    Invoke("MoveAgain", 1.0f);
                }
                else if (girlLife == 0)
                {
                    isDie = true;

                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                    Invoke("ActiveSound",1.0f);

                    girlLife = minGirlLife;

                    Invoke("RunAway", 5.0f);
                    General.isEnemyDead = true;

                    if (minGirlLife == 0)
                    {
                        General.currentEnemies = General.currentEnemies + 1;
                        enemiesInventary.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentEnemies.ToString();

                    }
                }
            }
        }
    }
}
