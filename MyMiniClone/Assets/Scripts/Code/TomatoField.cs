using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoField : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public List<Transform> branches;
    public int maxTomatoes = 3;
    public float growthTime = 5f;
    public Collider takeTomatoesTrigger;
    public Transform playerTomatoesParent;
    private Vector3 tomatoOffset = new Vector3(0, 0.1f, 0);

private List<GameObject> tomatoes = new List<GameObject>();

private IEnumerator SpawnTomatoes()
{
    while (true)
    {
        yield return new WaitForSeconds(growthTime);

        if (tomatoes.Count < maxTomatoes)
        {
            int branchIndex = tomatoes.Count % branches.Count;
            Transform branch = branches[branchIndex];

            Vector3 spawnPos = branch.position + Vector3.up * 0.1f; // spawn tomato slightly above the branch
            GameObject tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity, branch); // set parent to the corresponding branch
            tomatoes.Add(tomato);
        }
    }
}

private void Start()
{
    StartCoroutine(SpawnTomatoes());

    // Make sure the takeTomatoesTrigger has a trigger collider
    takeTomatoesTrigger.isTrigger = true;
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered tomato field.");
    }
    else if (other == takeTomatoesTrigger)
    {
        // Take the top tomato from the list and add it to the player's stack
        if (tomatoes.Count > 0)
        {
            GameObject tomato = tomatoes[tomatoes.Count - 1];
            tomatoes.RemoveAt(tomatoes.Count - 1);
            tomato.transform.SetParent(playerTomatoesParent);
            tomato.transform.localPosition = tomatoOffset * (playerTomatoesParent.childCount - 1);
        }
    }
}
}