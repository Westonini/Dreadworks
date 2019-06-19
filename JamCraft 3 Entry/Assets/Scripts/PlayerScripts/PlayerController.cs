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

    private Vector3 moveDirection;

    public bool mouseOverPlayer = false;

    private Camera mainCam;

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
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * movementSpeed).normalized;
        
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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer) && !mouseOverPlayer)
        {
            Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookAtPosition);          
        }
    }

    public void OnMouseOver()
    {
        mouseOverPlayer = true;
    }

    public void OnMouseExit()
    {
        mouseOverPlayer = false;
    }
}
