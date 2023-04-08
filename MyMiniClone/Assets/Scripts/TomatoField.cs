using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoField : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public  List<Transform> branches;
    public int maxTomatoes = 3;
    public float growthTime = 5f;

    public List<GameObject> tomatoes = new List<GameObject>();

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
                GameObject tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);
                tomato.transform.parent = branch; // set parent to the corresponding branch
                tomatoes.Add(tomato);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnTomatoes());
    }
}