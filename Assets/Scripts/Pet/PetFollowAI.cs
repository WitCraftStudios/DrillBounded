using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class PetFollowAI : MonoBehaviour
{
    [Tooltip("Reference to the player transform.")]
    public Transform player;

    [Tooltip("Desired distance to keep from the player.")]
    public float followDistance = 3f;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (player == null)
            Debug.LogError("Player transform not assigned in PetFollowAI");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            if (!agent.hasPath || agent.destination != player.position)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }
        }
    }

    // Optional: Set the player at runtime
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
