using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float movementSpeed = 5.0f;
    [HideInInspector]
    public float resetMovementSpeed;
    public float jumpForce = 5.0f;
    [HideInInspector]
    public float sneakingSpeed;
    [HideInInspector]
    public static bool playerIsSneaking = false;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private Vector3 moveDirection;

    private Camera mainCam;
    

    private bool mouseOverEnemy = false;

    void Start()
    {
        mainCam = Camera.main;
        resetMovementSpeed = movementSpeed;
        sneakingSpeed = movementSpeed / 2;
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Movement
        if (ChangeCameraAngle.angleBeingSwitchedTo == "0")
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * movementSpeed).normalized;
        }
        else if (ChangeCameraAngle.angleBeingSwitchedTo == "90")
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Vertical") * movementSpeed, moveDirection.y, -Input.GetAxisRaw("Horizontal") * movementSpeed).normalized;
        }
        else if (ChangeCameraAngle.angleBeingSwitchedTo == "180")
        {
            moveDirection = new Vector3(-Input.GetAxisRaw("Horizontal") * movementSpeed, moveDirection.y, -Input.GetAxisRaw("Vertical") * movementSpeed).normalized;
        }
        else if (ChangeCameraAngle.angleBeingSwitchedTo == "270")
        {
            moveDirection = new Vector3(-Input.GetAxisRaw("Vertical") * movementSpeed, moveDirection.y, Input.GetAxisRaw("Horizontal") * movementSpeed).normalized;
        }


        //Sneaking
        if (Input.GetButton("Sneak"))
        {
            playerIsSneaking = true;
            movementSpeed = sneakingSpeed;
        }
        if (Input.GetButtonUp("Sneak") && !UseConsumableItem.playerIsHealing)
        {
            playerIsSneaking = false;
            movementSpeed = resetMovementSpeed;
        }

        //Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * 0.35f);
        }
           
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        //Move character's y rotation to look at where mouse is pointing
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        //If character's mouse is on the ground and not on an enemy..
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer) && mouseOverEnemy == false)
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookAtPosition);
  
        }

        //If character's mouse is on an enemy...
        if (Physics.Raycast(ray, out RaycastHit hitEnemy, Mathf.Infinity, enemyLayer))
        {
            mouseOverEnemy = true;
            GameObject hitEnemyObject = hitEnemy.transform.gameObject;
            Transform enemyLocation = hitEnemyObject.transform;
            Vector3 lookAtPosition = new Vector3(enemyLocation.transform.position.x, transform.position.y, enemyLocation.transform.position.z);
            transform.LookAt(lookAtPosition);
        }
        else
        {
            mouseOverEnemy = false;
        }
    }
}
