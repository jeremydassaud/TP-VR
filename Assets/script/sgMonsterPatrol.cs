using UnityEngine;
using UnityEngine.AI;

public class sgMonsterPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChooseNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            ChooseNextPoint();
        }
    }

    void ChooseNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
        agent.SetDestination(currentTarget.position);
    }
}
