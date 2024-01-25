using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBullet : MonoBehaviour
{
 public GameObject rainbowExplosion;
    private GameObject rainbowExplosionClon;

    void Start()
    {

    }

    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        rainbowExplosionClon = (GameObject)Instantiate(rainbowExplosion, this.gameObject.GetComponent<Transform>().position, this.gameObject.GetComponent<Transform>().rotation);
        Destroy(this.gameObject);
    }
}
