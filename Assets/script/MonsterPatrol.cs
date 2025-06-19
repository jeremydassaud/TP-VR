using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
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

    public void ChooseNextPoint()
    {
        if (patrolPoints.Length == 0) return;
        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
        agent.SetDestination(currentTarget.position);
    }
}
