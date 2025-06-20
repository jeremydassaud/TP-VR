using UnityEngine;
using UnityEngine.AI;

public class MonsterVision : MonoBehaviour
{
    public float visionRange = 15f;
    public float visionAngle = 60f;
    public Transform player;
    public LayerMask obstacleMask;

    private NavMeshAgent agent;
    private MonsterPatrol patrol;
    private bool isChasing = false;
    private float lostPlayerTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<MonsterPatrol>();
    }

    void Update()
    {
        Vector3 target = new Vector3(player.position.x, transform.position.y, player.position.z);

        if(CanSeePlayer())
        {
            if(!isChasing)
            {
                isChasing = true;
                patrol.enabled = false;
            }

            lostPlayerTimer = 0f;
            agent.SetDestination(target);
        } else if(isChasing)
        {
            lostPlayerTimer += Time.deltaTime;

            if(lostPlayerTimer <= 5f)
            {
                agent.SetDestination(target);
            } else
            {
                isChasing = false;
                patrol.enabled = true;
                patrol.ChooseNextPoint();
            }
        }
    }

    public void ForceStopChase()
    {
        isChasing = false;
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if(Vector3.Distance(transform.position, player.position) < visionRange && angle < visionAngle / 2f)
        {
            if(!Physics.Raycast(transform.position + Vector3.up, dirToPlayer, Vector3.Distance(transform.position, player.position), obstacleMask))
            {
                return true;
            }
        }

        return false;
    }
}
