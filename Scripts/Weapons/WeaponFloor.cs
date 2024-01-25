using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFloor : MonoBehaviour
{
    private float velocidad;

    void Start()
    {
        velocidad = 8.0f;
    }

    void Update()
    {
        this.gameObject.GetComponent<Transform>().Rotate(0.0f,velocidad * Time.deltaTime, 0.0f);
    }
}
