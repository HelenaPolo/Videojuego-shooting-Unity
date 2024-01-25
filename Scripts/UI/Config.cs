using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{

        public GameObject textSound;
        public Sprite Buttons_4;
        public Sprite Buttons_3;
   
    void Start()
    {
        if (PlayerPrefs.HasKey("ACTIVESOUND"))
        {
            if (PlayerPrefs.GetInt("ACTIVESOUND") == 0)
            {
                General.activeSound = false;
            }
            else
            {
                General.activeSound = true;
            }
        }

        if (General.activeSound)
        {
            textSound.gameObject.GetComponent<Text>().text = "Sound On";
            textSound.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<Image>().sprite = Buttons_4;

        }
        else
        {
            textSound.gameObject.GetComponent<Text>().text = "Sound Off";
            textSound.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<Image>().sprite = Buttons_3;
        }




    }

    public void ChangeActiveSound()
    {

        General.activeSound = !General.activeSound;

        if (General.activeSound)
        {
            PlayerPrefs.SetInt("ACTIVESOUND", 1);
            textSound.gameObject.GetComponent<Text>().text = "Sound On";
            textSound.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<Image>().sprite = Buttons_4;

        }
        else
        {
            PlayerPrefs.SetInt("ACTIVESOUND", 0);
            textSound.gameObject.GetComponent<Text>().text = "Sound Off";
            textSound.gameObject.GetComponent<Transform>().parent.gameObject.GetComponent<Image>().sprite = Buttons_3;
        }

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }







}
