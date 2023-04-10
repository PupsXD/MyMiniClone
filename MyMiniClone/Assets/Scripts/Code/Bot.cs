using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public List<GameObject> points;
    public GameObject checkout;
    public GameObject finish;

    private UnityEngine.AI.NavMeshAgent agent;

    private int currentPointIndex = 0;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPoint();
    }

    private void GoToNextPoint()
    {
        if (currentPointIndex < points.Count)
        {
            agent.SetDestination(points[currentPointIndex].transform.position);
        }
        else if (checkout != null)
        {
            agent.SetDestination(checkout.transform.position);
        }
        else if (finish != null)
        {
            agent.SetDestination(finish.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }

        currentPointIndex++;
    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bot has been destroyed");
    }
}