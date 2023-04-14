using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracking : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - gameCamera.transform.position);
    }

}
