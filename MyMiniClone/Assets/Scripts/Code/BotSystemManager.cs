using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSystemManager : MonoBehaviour
{
    public GameObject botSystemPrefab;
    public int maxClones = 3;
    public float checkInterval = 1f;

    private void Start()
    {
        int initialCloneCount = Random.Range(1, 4);

        for (int i = 0; i < initialCloneCount; i++)
        {
            CreateBotSystemClone();
        }

        StartCoroutine(CheckClones());
    }

    private IEnumerator CheckClones()
    {
        while (true)
        {
            int activeClones = GameObject.FindGameObjectsWithTag("BotSystem").Length;

            if (activeClones < maxClones)
            {
                CreateBotSystemClone();
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    public void CreateBotSystemClone()
    {
        GameObject botSystemClone = Instantiate(botSystemPrefab);
        botSystemClone.SetActive(true);
    }
}