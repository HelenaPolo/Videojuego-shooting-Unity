using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController3D : MonoBehaviour
{
    public GameObject petalWeapon;
    public GameObject bubbleWeapon;
    public GameObject rainbowWeapon;
    public bool[] stateWeapons;

    public GameObject petalBall;
    public GameObject ballCreator;
    private GameObject petalBallClon;
    public GameObject bubbleBall;
    public GameObject bubbleCreator;
    private GameObject bubbleBallClon;
    public GameObject rainbowBall;
    public GameObject rainbowCreator;
    private GameObject rainbowBallClon;

    public GameObject sight;
    public GameObject hand;
    private Vector3 middleScreen;

    public GameObject inventary;
    public GameObject collectionables;
    public GameObject enemies;
    public GameObject weapons;
    public GameObject frontCage;
    private bool isCage;

    public GameObject petalBallsNum;
    public GameObject rainbowBallsNum;
    public GameObject bubbleBallsNum;

    private string petalBallsNumFormat;
    private string rainbowBallsNumFormat;
    private string bubbleBallsNumFormat;

    public GameObject petalFloor;
    public GameObject bubbleFloor;
    public GameObject rainbowFloor;

    private float secCall;

    public GameObject bubbleFan;


    private float maxFrecuencyPetalBall;
    private float maxFrecuencyBubbleBall;
    private float maxFrecuencyRainbowBall;

    private float ballForce;
	private float bubbleForce;

    private RaycastHit hit;
    private float rayDistance;
    public float rayDistanceCollectionable;

    public GameObject lifeBar;
    private int bossDamage;
  
    private bool blockPetalBall;
    private bool blockBubbleBall;
    private bool blockRainbowBall;

    public GameObject exitPortal1;
    public GameObject exitPortal2;
    public GameObject portal1;
    public GameObject portalMessage;

    private float monkeyBarPos;
    public GameObject monkeyBar;

    public int powerBalls;
    public GameObject bossTrigger;
    public GameObject bridge1;
    public GameObject bridge2;
    public GameObject limit1;
    public GameObject limit2;



    public GameObject weaponFloorSound;
    public GameObject noWeaponSound;
    public GameObject petalBallSound;
    public GameObject noBulletSound;
    public GameObject bubbleBallSound;
    public GameObject rainbowBallSound;
    public GameObject chargerSound;
    public GameObject chargerLifeSound;
    public GameObject damageSound;
    public GameObject inventarySound;
    public GameObject portalOnSound;
    public GameObject portalOffSound;
    public GameObject powerBallSound;
    public GameObject bridgeSound;

    public void FormatPetalBallsText()
    {
        petalBallsNumFormat = "";
        if (General.currentPetalBall < 10)
        {
            petalBallsNumFormat += "0";
        }
        petalBallsNumFormat += General.currentPetalBall.ToString() + " / ";
        if (General.maxPetalBall < 10)
        {
            petalBallsNumFormat += "0";
        }
        petalBallsNumFormat += General.maxPetalBall;

        petalBallsNum.gameObject.GetComponent<Text>().text = petalBallsNumFormat;

    }
    public void FormatBubbleBallsText()
    {
        bubbleBallsNumFormat = "";
        if (General.currentBubbleBall < 10)
        {
            bubbleBallsNumFormat += "0";
        }
        bubbleBallsNumFormat += General.currentBubbleBall.ToString() + " / ";
        if (General.maxBubbleBall < 10)
        {
            bubbleBallsNumFormat += "0";
        }
        bubbleBallsNumFormat += General.maxBubbleBall;

        bubbleBallsNum.gameObject.GetComponent<Text>().text = bubbleBallsNumFormat;

    }
    public void FormatRainbowBallsText()
    {
        rainbowBallsNumFormat = " ";
        if (General.currentRainbowBall < 10)
        {
            rainbowBallsNumFormat += "0";
        }
        rainbowBallsNumFormat += General.currentRainbowBall.ToString() + " / ";
        if (General.maxRainbowBall < 10)
        {
            rainbowBallsNumFormat += "0";
        }
        rainbowBallsNumFormat += General.maxPetalBall;

        rainbowBallsNum.gameObject.GetComponent<Text>().text = rainbowBallsNumFormat;
    }

    void Start()
    {
        General.inventary = new int[4];
        for(int i=0; i < General.inventary.Length; i++)
        {
            General.inventary[i] = 0;
        }

        General.currentPlayerLife = General.maxPlayerLife;
        lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
        FormatPetalBallsText();
        FormatRainbowBallsText();
        FormatRainbowBallsText();

        petalBallsNum.gameObject.SetActive(false);
        bubbleBallsNum.gameObject.SetActive(false);
        rainbowBallsNum.gameObject.SetActive(false);

        rayDistance = 100.0f;
        rayDistanceCollectionable = 5.0f;

        sight.gameObject.SetActive(false);
        hand.gameObject.SetActive(false);

        stateWeapons = new bool[3];
        stateWeapons[0] = false;
        stateWeapons[1] = false;
        stateWeapons[2] = false;
        ballForce = 30.0f;
		bubbleForce = 1.0f;
        maxFrecuencyPetalBall = 1.0f;
        blockPetalBall = false;

        maxFrecuencyBubbleBall = 1.0f;
        blockBubbleBall = false;

        maxFrecuencyRainbowBall = 1.0f;
        blockRainbowBall = false;

        secCall = 15.0f;

        powerBalls = 0;
        bossTrigger.SetActive(false);
        bossDamage = 5;

        isCage = false;

        bridge1.gameObject.SetActive(false);
        bridge2.gameObject.SetActive(false);

        monkeyBarPos = 4.0f;
    }

    public void UnBlockPetalBall()
    {
        blockPetalBall = false;
    }
    public void UnBlockBubbleBall()
    {
        blockBubbleBall = false;
        //bubbleFan.gameObject.GetComponent<Animator>().ResetTrigger("BubbleFanOn");
        bubbleFan.gameObject.GetComponent<Animator>().SetBool("BubbleFanRotate",false);
    }
    public void UnBlockRainbowBall()
    {
        blockRainbowBall = false;
    }
    public void ShootPetalWeapon()
    {
        if (General.currentPetalBall > 0)
        {

            if (!blockPetalBall)
            {
                if (stateWeapons[1] && General.currentWeapon == 2)
                {
                    General.currentPetalBall--;

                    FormatPetalBallsText();

                    petalBallSound.gameObject.GetComponent<AudioSource>().Play();
                    petalBallClon = (GameObject)Instantiate(petalBall, ballCreator.gameObject.GetComponent<Transform>().position, petalWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().rotation);
                    petalBallClon.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().forward * ballForce;
                    Destroy(petalBallClon, 5.0f);
                    blockPetalBall = true;
                    Invoke("UnBlockPetalBall", maxFrecuencyPetalBall);
                    //petalFloor.gameObject.SetActive(true);
                    weapons.gameObject.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentPetalBall.ToString();

                }
            }
        }
        else
        {
            noBulletSound.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void ShootBubbleWeapon()
    {
        if (General.currentBubbleBall > 0)
        {

            if (!blockBubbleBall)
            {
                if (stateWeapons[0] && General.currentWeapon == 1)
                {
                    General.currentBubbleBall--;

                    FormatBubbleBallsText();

                    bubbleBallSound.gameObject.GetComponent<AudioSource>().Play();
                    bubbleBallClon = (GameObject)Instantiate(bubbleBall, bubbleCreator.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().rotation);
                    //bubbleBallClon.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().forward * bubbleForce;
                    Destroy(bubbleBallClon, 3.0f);
                    blockBubbleBall = true;
                    //bubbleFan.gameObject.GetComponent<Animator>().SetTrigger("BubbleFanOn");
                    bubbleFan.gameObject.GetComponent<Animator>().SetBool("BubbleFanRotate", true);
                    Invoke("UnBlockBubbleBall", maxFrecuencyBubbleBall);
                    //bubbleFloor.gameObject.SetActive(true);
                    weapons.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentBubbleBall.ToString();

                }
            }
        }
        else
        {
            noBulletSound.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void ShootRainbowWeapon()
    {
        if (General.currentRainbowBall > 0)
        {

            if (!blockRainbowBall)
            {
                if (stateWeapons[2] && General.currentWeapon == 3)
                {

                    General.currentRainbowBall--;

                    FormatRainbowBallsText();

                    rainbowBallSound.gameObject.GetComponent<AudioSource>().Play();
                    rainbowBallClon = (GameObject)Instantiate(rainbowBall, rainbowCreator.gameObject.GetComponent<Transform>().position, rainbowWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().rotation);
                    rainbowBallClon.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().forward * ballForce;
                    //rainbowBallClon = (GameObject)Instantiate(rainbowBall, rainbowCreator.gameObject.GetComponent<Transform>().position, rainbowWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().rotation);
                    Destroy(rainbowBallClon, 3.0f);
                    blockRainbowBall = true;
                    Invoke("UnBlockRainbowBall", maxFrecuencyRainbowBall);
                    weapons.gameObject.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentRainbowBall.ToString();

                    //rainbowFloor.gameObject.SetActive(true); 
                    if (Physics.Raycast(rainbowWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().position, rainbowWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().forward, out hit, rayDistance))
                    {
                        Debug.DrawLine(rainbowWeapon.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().position, hit.point, Color.red);

                    }
                }
            }
        }
        else
        {
            noBulletSound.gameObject.GetComponent<AudioSource>().Play();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (stateWeapons[0] && !isCage)
            {
                bubbleWeapon.gameObject.SetActive(true);
                petalWeapon.gameObject.SetActive(false);
                rainbowWeapon.gameObject.SetActive(false);
                General.currentWeapon = 1;
                petalBallsNum.gameObject.SetActive(false);
                bubbleBallsNum.gameObject.SetActive(true);
                rainbowBallsNum.gameObject.SetActive(false);
                sight.gameObject.SetActive(true);
            }
            else
            {
                noWeaponSound.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (stateWeapons[1] && !isCage)
            {
                bubbleWeapon.gameObject.SetActive(false);
                petalWeapon.gameObject.SetActive(true);
                rainbowWeapon.gameObject.SetActive(false);
                General.currentWeapon = 2;
                petalBallsNum.gameObject.SetActive(true);
                bubbleBallsNum.gameObject.SetActive(false);
                rainbowBallsNum.gameObject.SetActive(false);
                sight.gameObject.SetActive(true);

            }
            else
            {
                noWeaponSound.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (stateWeapons[2] && !isCage)
            {
                bubbleWeapon.gameObject.SetActive(false);
                petalWeapon.gameObject.SetActive(false);
                rainbowWeapon.gameObject.SetActive(true);
                General.currentWeapon = 3;
                petalBallsNum.gameObject.SetActive(false);
                bubbleBallsNum.gameObject.SetActive(false);
                rainbowBallsNum.gameObject.SetActive(true);
                sight.gameObject.SetActive(true);

            }
            else
            {
                noWeaponSound.gameObject.GetComponent<AudioSource>().Play();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            ShootPetalWeapon();
            ShootBubbleWeapon();
            ShootRainbowWeapon();

        }

        Collectionable();

        if (General.isEnemyDead)
        {
            General.deadEnemies++;
            General.isEnemyDead = false;
        }
      
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "WeaponFloor")
        {
            Destroy(other.gameObject);
            stateWeapons[0] = true;
            weaponFloorSound.gameObject.GetComponent<AudioSource>().Play();

            if (!petalWeapon.gameObject.activeSelf && !rainbowWeapon.gameObject.activeSelf && !isCage)
            {
                General.currentWeapon = 1;
                bubbleWeapon.gameObject.SetActive(true);
                bubbleBallsNum.gameObject.SetActive(true);
                sight.gameObject.SetActive(true);

            }
        }
        if (other.gameObject.tag == "WeaponFloor2")
        {
            Destroy(other.gameObject);
            stateWeapons[1] = true;
            weaponFloorSound.gameObject.GetComponent<AudioSource>().Play();

            if (!bubbleWeapon.gameObject.activeSelf && !rainbowWeapon.gameObject.activeSelf && !isCage)
            {
                General.currentWeapon = 2;
                petalWeapon.gameObject.SetActive(true);
                petalBallsNum.gameObject.SetActive(true);
                sight.gameObject.SetActive(true);

            }

        }
        if (other.gameObject.tag == "WeaponFloor3")
        {
            Destroy(other.gameObject);
            stateWeapons[2] = true;
            weaponFloorSound.gameObject.GetComponent<AudioSource>().Play();

            if (!bubbleWeapon.gameObject.activeSelf && !petalWeapon.gameObject.activeSelf && !isCage)
            {
                General.currentWeapon = 3;
                rainbowWeapon.gameObject.SetActive(true);
                rainbowBallsNum.gameObject.SetActive(true);
                sight.gameObject.SetActive(true);


            }
        }

        if (other.gameObject.name == "PetalFloor" && General.currentPetalBall < 10)
        {
            General.currentPetalBall = General.currentPetalBall + General.chargerPetalBall;
            chargerSound.gameObject.GetComponent<AudioSource>().Play();
            //Destroy(other.gameObject);
            petalFloor.gameObject.SetActive(false);
            StartCoroutine(PetalFloorCall());

            if (General.currentPetalBall > General.maxPetalBall)
            {
                General.currentPetalBall = General.maxPetalBall;

            }
            FormatPetalBallsText();
            weapons.gameObject.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentPetalBall.ToString();

        }

        if (other.gameObject.name == "RainbowFloor" && General.currentRainbowBall < 10)
        {
            General.currentRainbowBall = General.currentRainbowBall + General.chargerRainbowBall;
            chargerSound.gameObject.GetComponent<AudioSource>().Play();
            //Destroy(other.gameObject);
            rainbowFloor.gameObject.SetActive(false);
            StartCoroutine(RainbowFloorCall());

            if (General.currentRainbowBall > General.maxRainbowBall)
            {
                General.currentRainbowBall = General.maxRainbowBall;

            }
            FormatRainbowBallsText();

            weapons.gameObject.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentRainbowBall.ToString();

        }

        if (other.gameObject.name == "BubbleFloor" && General.currentBubbleBall < 10)
        {
            General.currentBubbleBall = General.currentBubbleBall + General.chargerBubbleBall;
            chargerSound.gameObject.GetComponent<AudioSource>().Play();
            //Destroy(other.gameObject);
            bubbleFloor.gameObject.SetActive(false);
            StartCoroutine(BubbleFloorCall());

            if (General.currentBubbleBall > General.maxBubbleBall)
            {
                General.currentBubbleBall = General.maxBubbleBall;
            }

            FormatBubbleBallsText();
            weapons.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.currentBubbleBall.ToString();

        }
        if (other.gameObject.tag == "MedicalCupcake")
        {
            if (General.currentPlayerLife < General.maxPlayerLife)
            {
                General.currentPlayerLife = General.currentPlayerLife + General.chargerPlayerLife;
                if (General.currentPlayerLife > General.maxPlayerLife)
                {
                    General.currentPlayerLife = General.maxPlayerLife;
                }
                lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
                Destroy(other.gameObject);
                chargerLifeSound.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        if (other.gameObject.tag == "MissileTurret")
        {
            Destroy(other.gameObject);

            if (General.currentPlayerLife > General.minPlayerLife)
            {
                General.currentPlayerLife -= 5;
                damageSound.gameObject.GetComponent<AudioSource>().Play();
                if (General.currentPlayerLife <= General.minPlayerLife)
                {
                    Debug.Log("Player dead");
                    General.currentPlayerLife = General.minPlayerLife;
                }
                lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;

            }
            else
            {
                Invoke("GoToLose", 1.0f);
            }
        }
        if (other.gameObject.tag == "EnemyWeapon")
        {
            if (!damageSound.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                if (General.currentPlayerLife > General.minPlayerLife)
                {
                    General.currentPlayerLife--;
                    lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
                    damageSound.gameObject.GetComponent<AudioSource>().Play();
                }

                else//if (General.currentPlayerLife <= General.minPlayerLife)
                {
                    Debug.Log("Player dead");
                    General.currentPlayerLife = General.minPlayerLife;
                    Invoke("GoToLose", 1.0f);
                }



            }
        }
        if (other.gameObject.tag == "BossAttack")
        {
            if (!damageSound.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                if (General.currentPlayerLife > General.minPlayerLife)
                {
                    General.currentPlayerLife = General.currentPlayerLife - bossDamage;
                    lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
                    damageSound.gameObject.GetComponent<AudioSource>().Play();
                }
                else
                {
                    Debug.Log("Player dead");
                    General.currentPlayerLife = General.minPlayerLife;

                    Invoke("GoToLose", 1.0f);
                }
            }
        }

        if (other.gameObject.tag == "Portal1")
        {
            if (isCage)
            {
                this.gameObject.GetComponent<CharacterController>().enabled = false;
                this.gameObject.GetComponent<Transform>().position = exitPortal2.gameObject.GetComponent<Transform>().position;
                this.gameObject.GetComponent<CharacterController>().enabled = true;
                portalOnSound.gameObject.GetComponent<AudioSource>().Play();
                if (frontCage)
                {
                    Destroy(frontCage.gameObject);
                }
                isCage = false;
                General.isNight = true;
            }
            else
            {
                portalMessage.SetActive(true);
            }
        }

            /* if (other.gameObject.tag == "Portal2")
             {
                 this.gameObject.GetComponent<CharacterController>().enabled = false;
                 this.gameObject.GetComponent<Transform>().position = exitPortal1.gameObject.GetComponent<Transform>().position;
                 this.gameObject.GetComponent<CharacterController>().enabled = true;
                 portalOnSound.gameObject.GetComponent<AudioSource>().Play();



             }*/
            if (other.gameObject.tag == "MonkeyBar")

            {
                this.gameObject.GetComponent<CharacterController>().enabled = false;
                transform.position = new Vector3(transform.position.x, monkeyBar.transform.position.y - monkeyBarPos, transform.position.z);

                this.gameObject.GetComponent<CharacterController>().enabled = true;
                Debug.Log("MonkeyBar");
            }

            if (other.gameObject.tag == "PowerBall")
            {
                powerBalls++;
                powerBallSound.gameObject.GetComponent<AudioSource>().Play();

                if (powerBalls == 1)
                {
                    bridge1.gameObject.SetActive(true);
                    bridgeSound.gameObject.GetComponent<AudioSource>().Play();
                    General.currentPlayerLife = General.maxPlayerLife;
                    lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
                    Destroy(limit1.gameObject);

                }
                if (powerBalls == 3)
                {
                    bridge2.SetActive(true);
                    bridgeSound.gameObject.GetComponent<AudioSource>().Play();
                    General.currentBubbleBall = General.maxBubbleBall;
                    General.currentPetalBall = General.maxPetalBall;
                    General.currentRainbowBall = General.maxRainbowBall;
                    FormatBubbleBallsText();
                    FormatPetalBallsText();
                    FormatRainbowBallsText();
                    Destroy(limit2.gameObject);

                }
                if (powerBalls == 4)
                {
                    bossTrigger.SetActive(true);
                }

                if (powerBalls == 5)
                {

                    // bossSmokeClon = (GameObject)Instantiate(bossSmoke, posBossSmoke.gameObject.GetComponent<Transform>().position, Quaternion.identity);
                    General.isBossActivate = true;

                }
                Destroy(other.gameObject);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            portalMessage.SetActive(false);
        }


        IEnumerator PetalFloorCall()
        {
            yield return new WaitForSeconds(secCall);
            petalFloor.gameObject.SetActive(true);
        }
        IEnumerator BubbleFloorCall()
        {
            yield return new WaitForSeconds(secCall);

            bubbleFloor.gameObject.SetActive(true);
        }
        IEnumerator RainbowFloorCall()
        {
            yield return new WaitForSeconds(secCall);
            rainbowFloor.gameObject.SetActive(true);

        }


    public void Collectionable()
    {
        middleScreen = this.gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        if(Physics.Raycast(middleScreen, this.gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Transform>().forward,out hit, rayDistanceCollectionable))
        {
            if (hit.collider.gameObject.tag == "Collectionable")
            {
                hand.gameObject.SetActive(true);
                Debug.DrawLine(middleScreen, hit.point, Color.yellow);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inventarySound.gameObject.GetComponent<AudioSource>().Play();
                    Destroy(hit.collider.gameObject);

                    General.collectionables = General.collectionables+1;
                    collectionables.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.collectionables.ToString();
                }
            }
            if (hit.collider.gameObject.tag == "Inventary1")
            {
                hand.gameObject.SetActive(true);
                Debug.DrawLine(middleScreen, hit.point, Color.yellow);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    inventarySound.gameObject.GetComponent<AudioSource>().Play();
                    General.inventary[0] = General.inventary[0] + 1;
                    inventary.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.inventary[0].ToString();
                }
            }
            if (hit.collider.gameObject.tag == "Inventary2")
            {
                hand.gameObject.SetActive(true);
                Debug.DrawLine(middleScreen, hit.point, Color.yellow);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    inventarySound.gameObject.GetComponent<AudioSource>().Play();
                    General.inventary[1] = General.inventary[1] + 1;
                    inventary.gameObject.GetComponent<Transform>().GetChild(1).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.inventary[1].ToString();
                }
            }
            if (hit.collider.gameObject.tag == "Inventary3")
            {
                hand.gameObject.SetActive(true);
                Debug.DrawLine(middleScreen, hit.point, Color.yellow);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    inventarySound.gameObject.GetComponent<AudioSource>().Play();
                    General.inventary[2] = General.inventary[2] + 1;
                    inventary.gameObject.GetComponent<Transform>().GetChild(2).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.inventary[2].ToString();
                }
            }
            if (hit.collider.gameObject.tag == "Inventary4")
            {
                hand.gameObject.SetActive(true);
                Debug.DrawLine(middleScreen, hit.point, Color.yellow);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.collider.gameObject);
                    inventarySound.gameObject.GetComponent<AudioSource>().Play();
                    General.inventary[3] = General.inventary[3] + 1;
                    inventary.gameObject.GetComponent<Transform>().GetChild(3).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.inventary[3].ToString();
                    //portal1.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    frontCage.gameObject.SetActive(true);
                    bubbleWeapon.gameObject.SetActive(false);
                    rainbowWeapon.gameObject.SetActive(false);
                    petalWeapon.gameObject.SetActive(false);
                    isCage = true;
                    sight.gameObject.SetActive(false);
            }
            }
        }
        else
        {
            hand.gameObject.SetActive(false);
        }
    }
    public void OnControllerColliderHit(ControllerColliderHit other)
    {
       
  
        if (other.gameObject.tag == "Damage")
        {
            Debug.Log("Hitting");
            if (!damageSound.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                if (General.currentPlayerLife > General.minPlayerLife)
                {
                    General.currentPlayerLife--;
                    lifeBar.gameObject.GetComponent<Slider>().value = General.currentPlayerLife;
                    damageSound.gameObject.GetComponent<AudioSource>().Play();
                    if (General.currentPlayerLife <= General.minPlayerLife)
                    {
                        Debug.Log("Player dead");
                        General.currentPlayerLife = General.minPlayerLife;
                    }

                }
                else
                {
                    Invoke("GoToLose", 1.0f);
                }
            }
        }
    }
    public void GoToLose()
    {
        SceneManager.LoadScene("Lose");
    }

}
