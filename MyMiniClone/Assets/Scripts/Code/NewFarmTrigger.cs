using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFarmTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Заплати денег");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Пошел нахуй эшуалли");
    }
}
