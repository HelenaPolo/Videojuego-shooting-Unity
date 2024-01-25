using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBalls : MonoBehaviour
{
    public GameObject bubbleExplosion;
    private GameObject bubbleExplosionClon;

    void Start()
    {

    }

    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        bubbleExplosionClon = (GameObject)Instantiate(bubbleExplosion, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
        Destroy(this.gameObject);
    }
}
