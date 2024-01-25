using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxNight : MonoBehaviour
{
    public Material skyNight;

    void Start()
    {
        
    }

    void Update()
    {
        if (General.isNight)
        {
            RenderSettings.skybox = skyNight;
        }
    }
}
