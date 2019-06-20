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

    Coroutine ChangeKinematicCoroutine;

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

    //Called in other scripts for when the enemy needs to get knocked back.
    public void Knockback(int knockback, GameObject hitby, bool reverseKnockbackDirection = false)
    {
        if (ChangeKinematicCoroutine != null)
        {
            StopCoroutine(ChangeKinematicCoroutine);
        }

        ChangeKinematicCoroutine = StartCoroutine(ChangeIsKinematic());
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;

        Vector3 direction;
        if (!reverseKnockbackDirection)
        {
            direction = (transform.position - hitby.transform.position).normalized;
        }
        else
        {
            direction = (hitby.transform.position - transform.position).normalized;
        }
        
        GetComponent<Rigidbody>().AddForce(direction * knockback);
    }

    public IEnumerator ChangeIsKinematic()
    {
        bool isKinematic = gameObject.GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(0.01f);

        if (gameObject != null)
        {
            isKinematic = true;
        }

    }
}
