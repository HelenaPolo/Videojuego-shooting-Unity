using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{


    public static int currentWeapon = 0;

    public static int maxPetalBall = 10;
    public static int maxBubbleBall = 10;
    public static int maxRainbowBall = 10;

    public static int currentPetalBall = 10;
    public static int currentBubbleBall = 10;
    public static int currentRainbowBall = 10;

    public static int chargerPetalBall = 5;
    public static int chargerBubbleBall = 5;
    public static int chargerRainbowBall = 5;

    public static int currentPlayerLife = 50;
    public static int maxPlayerLife =50;
    public static int minPlayerLife = 0;
    public static int chargerPlayerLife = 5;

    public static int min = 5;
    public static int sec = 0;
    public static int currentMin = 0;
    public static int currentSec = 0;

    public static int[] inventary;
    //public static int[] collectionables;
    //public static int[] enemies;
    public static int collectionables = 0;


    public static bool isBossActivate =false;

    public static int currentEnemies = 0;
    public static int deadEnemies = 0;
    public static int minDeadEnemies = 5;
    public static bool isEnemyDead = false;

    public static bool activeSound = true;

    public static bool isNight = false;
    private static float skyBoxSpeed;
   

    void Start()
    {
        skyBoxSpeed = 1.0f;
    }

  
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);

      
    }
}
