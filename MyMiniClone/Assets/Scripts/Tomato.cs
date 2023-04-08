using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public float growthTime = 5.0f;
   // public Color ripeColor;

    private Renderer tomatoRenderer;
    private float currentGrowthTime;
    private int branchIndex;

    private void Start()
    {
        tomatoRenderer = GetComponent<Renderer>();
        currentGrowthTime = 0.0f;
    }

    private void Update()
    {
        if (currentGrowthTime < growthTime)
        {
            currentGrowthTime += Time.deltaTime;
            float growthPercentage = currentGrowthTime / growthTime;
            //tomatoRenderer.material.color = Color.Lerp(Color.green, ripeColor, growthPercentage);
        }
    }

    public void SetBranchIndex(int index)
    {
        branchIndex = index;
        Transform branch = transform.parent;
        transform.position = branch.GetChild(index).position;
    }
}
