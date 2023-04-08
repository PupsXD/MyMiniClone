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
    public Collider placeTomatoesTrigger;
    public Transform playerTomatoesParent;
    public Transform shopTomatoesParent;
    public Vector3 tomatoOffset = new Vector3(0, 0.1f, 0);

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
                GameObject tomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);
                tomato.transform.parent = branch; // set parent to the corresponding branch
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
            Debug.Log("Нюхни жопу");
        }
        if (other == takeTomatoesTrigger || other.CompareTag("Player") )
        {
            
            // Take the top tomato from the list and add it to the player's stack
            if (tomatoes.Count > 0 && playerTomatoesParent.childCount < 3)
            {
                GameObject tomato = tomatoes[tomatoes.Count - 1];
                tomatoes.RemoveAt(tomatoes.Count - 1);
                tomato.transform.SetParent(playerTomatoesParent);
                tomato.transform.localPosition = tomatoOffset * playerTomatoesParent.childCount;
            }
        }
        else if (other == placeTomatoesTrigger)
        {
            // Place the player's tomatoes on the shop shelves
            int shelfIndex = 0;
            foreach (Transform shelf in shopTomatoesParent)
            {
                if (playerTomatoesParent.childCount == 0)
                {
                    break;
                }

                GameObject tomato = playerTomatoesParent.GetChild(0).gameObject;
                tomato.transform.SetParent(shelf);
                tomato.transform.localPosition = Vector3.zero;
                shelfIndex++;
            }
        }
    }
}