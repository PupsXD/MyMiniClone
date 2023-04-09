using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tomato")]
public class Tomato : ScriptableObject
{
    public string tomatoName = "Tomato";
    public GameObject TomatoPrefab;
    public float value = 1f;
}
