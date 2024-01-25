using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectionables : MonoBehaviour
{
    private float velocidad;
    public GameObject candySound;
    public GameObject collectionables;

    void Start()
    {
        velocidad = 20.0f;

    }
    void Update()
    {
        this.gameObject.GetComponent<Transform>().Rotate(0.0f, velocidad * Time.deltaTime, 0.0f);
    }

    private void OnCollisionEnter(Collision other)
    {
       /* if (other.gameObject.tag == "BubbleBalls")
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Break");
            candySound.gameObject.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject,0.8f);
        }*/
        if (other.gameObject.tag == "PetalBalls")
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Break");
            candySound.gameObject.GetComponent<AudioSource>().Play();
        
            General.collectionables = General.collectionables+1;
            collectionables.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.collectionables.ToString();

            Destroy(this.gameObject, 0.8f);
        }
        if (other.gameObject.tag == "RainbowBalls")
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Break");
            candySound.gameObject.GetComponent<AudioSource>().Play();
    
            General.collectionables = General.collectionables+1;
            collectionables.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.collectionables.ToString();


            Destroy(this.gameObject, 0.8f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "BubbleBalls")
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Break");
            candySound.gameObject.GetComponent<AudioSource>().Play();
          
            General.collectionables = General.collectionables+1;
            collectionables.gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Text>().text = General.collectionables.ToString();


            Destroy(this.gameObject, 0.8f);
        }
    }

}
