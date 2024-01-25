using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Crono : MonoBehaviour
{
    public GameObject cronoText;
    private string currentCronoText;

    public void FormatCronText()
    {

        currentCronoText = "";
        if (General.currentMin < 10)
        {
            currentCronoText = currentCronoText + "0";
        }
        currentCronoText = currentCronoText + General.currentMin.ToString() + ":";
        if (General.currentSec < 10)
        {
            currentCronoText = currentCronoText + "0";
        }
        currentCronoText = currentCronoText + General.currentSec.ToString();

        cronoText.gameObject.GetComponent<Text>().text = currentCronoText;
    }

    void Start()
    {
        General.currentMin = General.min;
        General.currentSec = General.sec;
        FormatCronText();     
        InvokeRepeating("Cron", 1.0f, 1.0f);
    }


    public void Cron()
    {
        General.currentSec = General.currentSec - 1;
        if (General.currentSec < 0)
        {
            General.currentMin = General.currentMin - 1;
            General.currentSec = 59;
        }
        FormatCronText();

        if (General.currentMin <= 0 && General.currentSec <= 0)
        {
            CancelInvoke("Cron");
            Debug.Log("Time is finish");
            Invoke("GoToLose", 1.0f);
        }

    }
    public void GoToLose()
    {
        SceneManager.LoadScene("Lose");
    }

}
