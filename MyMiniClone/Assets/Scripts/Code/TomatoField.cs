using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoField : MonoBehaviour
{
    public List<GameObject> Branches; // List of branches where tomatoes can spawn
    public GameObject TomatoPrefab; // Prefab of the tomato to spawn

    public List<GameObject> spawnedTomatoes = new List<GameObject>(); // List of spawned tomatoes

    public float spawnTime = 1;
    public float repeatTime = 3;
    private void Start()
    {
        InvokeRepeating("SpawnTomato", spawnTime, repeatTime); // Call SpawnTomato every repeatTime seconds
    }

    private void SpawnTomato()
    {
        if (spawnedTomatoes.Count < Branches.Count)
        {
            // Choose a random branch to spawn the tomato on
            GameObject branch = Branches[Random.Range(0, Branches.Count)];

            // Check if there is already a tomato spawned on the branch
            if (!spawnedTomatoes.Exists(tomato => tomato != null && tomato.transform.parent == branch.transform))
            {
                // Spawn the tomato on the branch
                GameObject tomato = Instantiate(TomatoPrefab, branch.transform.position, Quaternion.identity);
                tomato.transform.parent = branch.transform;
                spawnedTomatoes.Add(tomato);
            }
        }
    }

    public void RemoveTomato(GameObject tomato)
    {
        spawnedTomatoes.Remove(tomato);
    }
}
