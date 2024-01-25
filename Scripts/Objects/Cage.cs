using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private bool activeCage;
    private GameObject player3D;
    private float posZ;

    void Start()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
        posZ = 4.0f;
        activeCage = false;
    }

    void Update()
    {
        if (General.currentEnemies == General.minDeadEnemies)
        {
            if (!activeCage)
            {
                this.gameObject.GetComponent<Transform>().position = new Vector3(player3D.gameObject.GetComponent<Transform>().position.x, player3D.gameObject.GetComponent<Transform>().position.y, player3D.gameObject.GetComponent<Transform>().position.z + posZ);
                activeCage = true;
            }
            else
            {
                this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, this.gameObject.GetComponent<Transform>().position.z);

            }
        }

        /* if (Input.GetKeyDown(KeyCode.E))
         {
             Destroy(this.gameObject);
         }*/
    }
}