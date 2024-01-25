using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roundabout : MonoBehaviour
{

    private float velocidad;



    void Start()
    {
        velocidad = 10.0f;

    }


    void Update()
    {
        
        this.gameObject.GetComponent<Transform>().Rotate(0.0f, velocidad * Time.deltaTime, 0.0f);

    }



    private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Transform>().SetParent(this.gameObject.GetComponent<Transform>());
			//Debug.Log("Trigger In");

		}

	}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Transform>().SetParent(null);
            //Debug.Log("Trigger Out");
        }
    }


}
