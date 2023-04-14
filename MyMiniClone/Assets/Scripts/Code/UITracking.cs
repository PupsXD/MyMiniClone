using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;


    // private void Start()
    // {
    //     gameCamera = FindObjectOfType<>();
    // }

    void Update()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
        //transform.rotation = Quaternion.LookRotation(transform.position - gameCamera.transform.position);
    }

}
