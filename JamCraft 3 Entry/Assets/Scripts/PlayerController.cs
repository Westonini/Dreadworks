using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float movementSpeed = 5.0f;
    private float resetMovementSpeed;
    public float jumpForce = 5.0f;
    private float sneakingSpeed;
    public LayerMask groundLayer;

    private Vector3 moveDirection;

    public bool mouseOverPlayer = false;

    void Start()
    {
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
            movementSpeed = sneakingSpeed;
        }
        if (Input.GetButtonUp("Sneak"))
        {
            movementSpeed = resetMovementSpeed;
        }

        //Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * 0.35f);
        }
           
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        //Move character's y rotation to look at where mouse is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
