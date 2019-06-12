using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float movementSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public LayerMask boundariesLayer;

    private Vector3 moveDirection;

    public bool mouseOverPlayer = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        //Movement
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal") * movementSpeed, moveDirection.y, Input.GetAxisRaw("Vertical") * movementSpeed).normalized;

        //Jump
        /*if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }*/

        //Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * 0.35f);
        }
           
        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

        //Move character's y rotation to look at where mouse is pointing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, boundariesLayer) && !mouseOverPlayer)
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
