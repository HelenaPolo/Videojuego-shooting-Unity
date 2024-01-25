using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBallsDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Choco con " + other.gameObject.tag);

        if (other.gameObject.tag == "EnemyGirl")
        {

        }
    }
}
