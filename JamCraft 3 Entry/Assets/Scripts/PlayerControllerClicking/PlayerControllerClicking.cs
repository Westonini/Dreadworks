using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/* Controls the player. Here we chose our "focus" and where to move. */

public class PlayerControllerClicking : MonoBehaviour
{
    public LayerMask movementMask;      // The ground

    PlayerMotor motor;      // Reference to our motor
    Camera cam;             // Reference to our camera

    // Get references
    void Start()
    {
        transform.localScale = new Vector3(1, 1, -1);
        motor = GetComponent<PlayerMotor>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If we hit
            if (Physics.Raycast(ray, out hit, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }
    }
}
