using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 moveInput;
    private Vector3 velocity;
    private Camera mainCam;

    [Header("Visual")]
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject playerWeapon;
    [SerializeField] private GameObject mouseLocationReference;

    [Header("Movement stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Moves character left, right forward and back
        MoveCharacter();

        //Handles jump and gravity
        ApplyVerticalVelocity();

        //Rotate the player to mouse postion
        RotateCharacter();
    }

    private void RotateCharacter()
    {
        Vector3 mousePosition = GetMouseWorldPostion();
        Vector3 flatMousePosition = new Vector3(mousePosition.x,transform.position.y + 1.5f, mousePosition.z); 

        mouseLocationReference.transform.position = GetMouseWorldPostion();
        if(characterController.isGrounded)
            playerModel.transform.LookAt(flatMousePosition);
        else
            playerModel.transform.LookAt(mousePosition);
    }

    private Vector3 GetMouseWorldPostion()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        Vector3 mouseWorldPos = Vector3.zero;

        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit))
        {
            mouseWorldPos = hit.point;
        }

        return mouseWorldPos;
    }

    private (Vector3 right, Vector3 left) GetCameraVector()
    {
        //camera forward and right vectors:
        var forward = mainCam.transform.forward;
        var right = mainCam.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        return (right, forward);
    }

    private void MoveCharacter()
    {
        var (right, forward) = GetCameraVector();

        Vector3 xVector = right * moveInput.x;
        Vector3 yVector = forward * moveInput.y;

        Vector3 move = xVector + yVector;
        characterController.Move(move * speed * Time.deltaTime);
    }

    private void ApplyVerticalVelocity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }


    // ----- Input methods ------
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - is grounded: {characterController.isGrounded}");
        if(context.performed && characterController.isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
      

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

}
