using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetectionMovement : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public NavMeshAgent agent;

    public GameObject armsDown;
    public GameObject armsUp;

    [HideInInspector]
    public bool isWithinDetectionRange = false;

    private bool kinematicCooldown = false;

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

        if (gameObject.GetComponent<Rigidbody>().isKinematic == false && !kinematicCooldown)
        {
            kinematicCooldown = true;
            StartCoroutine(ChangeIsKinematic());
        }
    }

    //When player is detected
    public void DetectedPlayer()
    {
        ChangeArms("Up");
        if (player != null)
        {
            agent.SetDestination(player.transform.position);

            Vector3 lookAtPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(lookAtPosition);
        }
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
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

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
        yield return new WaitForSeconds(0.35f);

        if (this != null)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            kinematicCooldown = false;
        }

    }
}
