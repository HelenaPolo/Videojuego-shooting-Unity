using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private GameObject player3D;
    private float distanceToPlayer3D;
    private Vector3 posPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;
    private float speedMove;
    private bool bloqAttack;
    private int bossLife;
    private int minBossLife;
    private bool isDie;
    private bool isHitting;
    public GameObject posIn;
    private int pickAnumber;
    public GameObject bossSmoke;
    private GameObject bossSmokeClon;
    public GameObject posBossSmoke;
    private bool isSmoke;
    public GameObject bossSound;
    private GameObject enemyDeadSound;


    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        enemyDeadSound = (GameObject)GameObject.Find("EnemyDeadSound");

    }

    void Start()
    {
        speedRotation = 5.0f;
        bloqAttack = false;
        speedMove = 4.0f;
        bossLife = 20;
        minBossLife = 0;
        isDie = false;
        isHitting = false;
        isSmoke = false;
        this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;

    }

    void Update()
    {


        if (General.isBossActivate)
        {

            if (!this.gameObject.GetComponent<Animator>().enabled)
            {
                if (!isSmoke)
                {
                    bossSound.gameObject.GetComponent<AudioSource>().Play();
                    bossSmokeClon = (GameObject)Instantiate(bossSmoke, posBossSmoke.gameObject.GetComponent<Transform>().position, Quaternion.identity);
                    Destroy(bossSmokeClon.gameObject, 2.0f);
                    isSmoke = true;
                }

                this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posIn.gameObject.GetComponent<Transform>().position, speedMove * Time.deltaTime);
                //this.gameObject.GetComponent<Animator>().enabled = true;
                Invoke("EnableAnim", 5.0f);
            }

        }



        if (this.gameObject.GetComponent<Animator>().enabled)
        {

            posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);

            distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);
            this.gameObject.GetComponent<Animator>().SetFloat("Distance", distanceToPlayer3D);

            Debug.Log(distanceToPlayer3D.ToString());

            if (distanceToPlayer3D < 30.0f)
            {


                if (distanceToPlayer3D < 7.0f)
                {
                    speedMove = 0;


                    if (!bloqAttack)
                    {
                        pickAnumber = Random.Range(1, 3);
                        this.gameObject.GetComponent<Animator>().SetInteger("RandAttack", pickAnumber);

                        bloqAttack = true;
                        Invoke("DesBloqAttack", 5.0f);
                    }
                }
                else
                {
                    this.gameObject.GetComponent<Animator>().SetInteger("RandAttack", 0);

                    if (!isHitting)
                    {

                        speedMove = 4.0f;

                        this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);
                    }
                }

                 newRotation = Quaternion.LookRotation(posPlayer3D - this.gameObject.GetComponent<Transform>().position);
                 this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);
            }
        }

    }

    private void ActiveSound()
    {
        enemyDeadSound.gameObject.GetComponent<AudioSource>().Play();

    }

    public void GoToWin()
    {
        SceneManager.LoadScene("Win");
    }

    private void EnableAnim()
    {
        this.gameObject.GetComponent<Animator>().enabled = true;
    }

    public void DesBloqAttack()
    {
        bloqAttack = false;

    }

    private void MoveAgain()
    {
        isHitting = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isDie)
        {
            if (other.gameObject.tag == "PetalBalls" || other.gameObject.tag == "RainbowBalls")
            {
                bossLife = bossLife - 1;
                Destroy(other.gameObject);
                if (bossLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    Invoke("ActiveSound", 1.0f);

                    isHitting = true;
                    Invoke("MoveAgain", 0.5f);
                }
                else if (bossLife == 0)
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                    Invoke("ActiveSound", 2.0f);


                    bossLife = minBossLife;
                    Invoke("GoToWin", 3.0f);


                }
            }
        }
    }
   
    private void OnParticleCollision(GameObject other)
    {
        if (!isDie)
        {
            if (other.gameObject.tag == "BubbleBalls")
            {
                bossLife--;
                Destroy(other.gameObject);

                if (bossLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                    isHitting = true;
                    Invoke("MoveAgain", 1.0f);
                }
                else if (bossLife == 0)
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");

                    bossLife = minBossLife;
                    Invoke("GoToWin", 3.0f);


                }
            }
        }
    }
}