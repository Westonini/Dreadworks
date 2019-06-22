using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAnimation : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Determines if enemy should be doing walking animation or not.
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    anim.SetBool("isWalking", false);
                }
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
        }
    }
}
