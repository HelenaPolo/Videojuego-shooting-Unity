using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidIA : MonoBehaviour
{
    private GameObject player3D;
    private Vector3 posPlayer3D;
    private float distanceToPlayer3D;

    private void Awake()
    {
        player3D = (GameObject)GameObject.Find("FPSController");
    }
    void Start()
    {
        
    }

    void Update()
    {
        posPlayer3D = new Vector3(player3D.GetComponent<Transform>().position.x, this.gameObject.GetComponent<Transform>().position.y, player3D.GetComponent<Transform>().position.z);
        distanceToPlayer3D = Vector3.Distance(this.gameObject.GetComponent<Transform>().position, posPlayer3D);

        if (distanceToPlayer3D < 15.0f)
        {
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(posPlayer3D);

        }


    }
}
