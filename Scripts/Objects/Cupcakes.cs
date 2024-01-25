using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupcakes : MonoBehaviour
{
    private float verticalSpeed;

    void Start()
    {
        verticalSpeed = 0.3f;
    }

    void Update()
    {
        this.gameObject.GetComponent<Transform>().Translate(0.0f, verticalSpeed * Time.deltaTime, 0.0f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Up")
        {
            verticalSpeed= -0.3f;
        }
        if (other.gameObject.tag == "Down")
        {
            verticalSpeed = 0.3f;
        }

    }
}
