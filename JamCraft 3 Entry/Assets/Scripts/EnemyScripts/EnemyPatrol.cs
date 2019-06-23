using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private float resetAgentAcceleration;
    private float resetAgentSpeed;
    EnemyDetectionMovement EDM;

    private float timeToStay;
    [HideInInspector]
    public bool startWait = false;
    private bool choseATime = false;

    void Awake()
    {
        EDM = gameObject.GetComponent<EnemyDetectionMovement>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        resetAgentAcceleration = agent.acceleration;
        resetAgentSpeed = agent.speed;
        //Slow down the agent while patrolling
        agent.acceleration = 3;
        agent.speed = 2;

        destPoint = Random.Range(0, points.Length);
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose a random point in the array as the destination,
        destPoint = Random.Range(0, points.Length);
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            if (!choseATime)
            {
                timeToStay = Random.Range(1, 6);
                startWait = true;
                choseATime = true;
            }

            if (startWait == false)
            {
                GotoNextPoint();
                choseATime = false;
            }
        }
         
        //Time that agent stays at spot.
        if (startWait == true)
        {          
            timeToStay -= Time.deltaTime;

            if (timeToStay <= 0)
            {
                startWait = false;
            }
        }

        // Reset movement and acceleration and disable this script if the player gets detected.
        if (EDM.isWithinDetectionRange)
        {
            agent.acceleration = resetAgentAcceleration;
            agent.speed = resetAgentSpeed;
            agent.autoBraking = true;

            enabled = !enabled;
        }
    }
}
