using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossIA : MonoBehaviour
{
    private GameObject player3D;
    private float distanceToPlayer3D;
    private Vector3 posPlayer3D;
    private Quaternion newRotation;
    private float speedRotation;
    private float speedMove;
    private bool bloqAttack;
    private int bossLife;
    private bool isDie;
    private bool isHitting;
    public GameObject posIn;
    private int pickAnumber;
    public bool isFloating;
    public GameObject bossSmoke;
    private GameObject bossSmokeClon;
    public GameObject posBossSmoke;
    private bool isLanded;






    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        //posIn = (GameObject)GameObject.Find("PosBossIn");

    }

    void Start()
    {
        speedRotation = 5.0f;
        bloqAttack = false;
        speedMove = 4.0f;
        bossLife = 20;
        isDie = false;
        isHitting = false;
        isFloating = false;
        isLanded = false;
        this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PosBossIn")
        {
            this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);

            //this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x,posIn.gameObject.GetComponent<Transform>().position.y,this.gameObject.GetComponent<Transform>().position.z);
            this.gameObject.GetComponent<Animator>().SetTrigger("Landed");
            isLanded = true;

        }
    }

    void Update()
    {


        if (General.isBossActivate)
        {

            if (!this.gameObject.GetComponent<Animator>().enabled)
            {
                //isFloating = true;
                bossSmokeClon = (GameObject)Instantiate(bossSmoke, posBossSmoke.gameObject.GetComponent<Transform>().position, Quaternion.identity);
                this.gameObject.GetComponent<Animator>().enabled = true;
                this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;

              //  if (isFloating)
                //{
                    this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posIn.gameObject.GetComponent<Transform>().position, speedMove * Time.deltaTime);
                    //isFloating = false;
                  //  Debug.Log(posIn.gameObject.GetComponent<Transform>().position.ToString());
                  
               // }
            }
        }

     

            if (this.gameObject.GetComponent<Animator>().enabled && isLanded)
            {

                posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);

                distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);
                this.gameObject.GetComponent<Animator>().SetFloat("Distance", distanceToPlayer3D);



                if (distanceToPlayer3D < 25.0f)
                {


                    if (distanceToPlayer3D < 5.0f)
                    {
                        speedMove = 0;


                        if (!bloqAttack)
                        {
                            pickAnumber = Random.Range(1, 3);
                            this.gameObject.GetComponent<Animator>().SetInteger("RandAttack", pickAnumber);

                            bloqAttack = true;
                            Invoke("DesBloqAttack", 3.0f);
                        }
                    }
                    else
                    {
                        if (!isHitting)
                        {
                            this.gameObject.GetComponent<Animator>().SetInteger("RandAttack", 0);
                            speedMove = 4.0f;

                            this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);
                        }
                    }

                    // newRotation = Quaternion.LookRotation(posPlayer3D - this.gameObject.GetComponent<Transform>().position);
                    // this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);
                }
            }

        }




 /*   public void IsKinematic()
    {
       // this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        if (!isDie)
        {

            posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);

            distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);
            this.gameObject.GetComponent<Animator>().SetFloat("Distance", distanceToPlayer3D);

            Debug.Log(distanceToPlayer3D.ToString());


            if (distanceToPlayer3D < 25.0f)
            {


                if (distanceToPlayer3D < 5.0f)
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
                    if (!isHitting)
                    {
                        this.gameObject.GetComponent<Animator>().SetInteger("RandAttack", 0);

                        this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);
                    }
                }

                //newRotation = Quaternion.LookRotation(posPlayer3D - this.gameObject.GetComponent<Transform>().position);
                //this.gameObject.GetComponent<Transform>().rotation = Quaternion.Slerp(this.gameObject.GetComponent<Transform>().rotation, newRotation, speedRotation * Time.deltaTime);
            }
        }
    }*/

    public void DesBloqAttack()
    {
        bloqAttack = false;

    }


    private void MoveAgain()
    {
        isHitting = false;
    }

 /*   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PosBossIn")
        {
            this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);

            //this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x,posIn.gameObject.GetComponent<Transform>().position.y,this.gameObject.GetComponent<Transform>().position.z);
            this.gameObject.GetComponent<Animator>().SetTrigger("Landed");
            isLanded = true;

        }
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (General.isBossActivate)

        {
           /* if (other.gameObject.tag == "PosBossIn") {
                this.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(this.gameObject.GetComponent<Transform>().position, posPlayer3D, speedMove * Time.deltaTime);

                //this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x,posIn.gameObject.GetComponent<Transform>().position.y,this.gameObject.GetComponent<Transform>().position.z);
                this.gameObject.GetComponent<Animator>().SetTrigger("Landed");

            }*/
            if (other.gameObject.tag == "PetalBalls" || other.gameObject.tag == "RainbowBalls")
            {
                bossLife--;
                Destroy(other.gameObject);
                if (bossLife > 0)
                {
                    this.gameObject.GetComponent<Animator>().SetTrigger("Damage");
                    isHitting = true;
                    Invoke("MoveAgain", 1.0f);
                }
                else
                {
                    isDie = true;
                    this.gameObject.GetComponent<Animator>().SetTrigger("Die");
                    Invoke("GoToWin", 3.0f);
                }
            }
        }
    }

    public void GoToWin()
    {
        SceneManager.LoadScene("Win");
    }

    private void OnParticleCollision(GameObject other)
    {
        if (General.isBossActivate)
        {

            if (!isDie)
            {
                if (other.gameObject.tag == "BubbleBalls")
                {
                    bossLife--;
                    Destroy(other.gameObject);

                    if (bossLife > 0)
                    {
                        this.gameObject.GetComponent<Animator>().SetTrigger("Damage");
                        isHitting = true;
                        Invoke("MoveAgain", 1.0f);
                    }
                    else
                    {
                        isDie = true;
                        this.gameObject.GetComponent<Animator>().SetTrigger("Die");

                    }
                }
            }
        }
    }
}


