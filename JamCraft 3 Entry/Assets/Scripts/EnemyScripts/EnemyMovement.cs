using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;

    public GameObject armsDown;
    public GameObject armsUp;

    [HideInInspector]
    public bool isWithinDetectionRange = false;

    EnemyHealth EH;

    void Awake()
    {
        EH = gameObject.GetComponent<EnemyHealth>();
        agent = gameObject.GetComponent<NavMeshAgent>();       
    }


    // Update is called once per frame
    void Update()
    {
        //If player gets within aggro range or if the enemy loses some hp, call DetectedPlayer().
        if (isWithinDetectionRange == true || (EH.health > 0 && EH.health < EH.maxHealth))
        {          
            DetectedPlayer();
        }
    }

    //When player is detected
    public void DetectedPlayer()
    {
        ChangeArms("Up");
        agent.SetDestination(player.transform.position);

        Vector3 lookAtPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookAtPosition);
    }

    //Changes arm position
    void ChangeArms(string direction)
    {
        if (direction == "Up")
        {
            armsDown.SetActive(false);
            armsUp.SetActive(true);
        }
        if (direction == "Down")
        {
            armsDown.SetActive(true);
            armsUp.SetActive(false);
        }
    }
}
