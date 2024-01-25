using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalBalls : MonoBehaviour
{

    public GameObject ballExplosion;
    private GameObject ballExplosionClon;

    void Start()
    {
        
    }
     
    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        ballExplosionClon = (GameObject)Instantiate(ballExplosion, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
        Destroy(this.gameObject);
    }
}
