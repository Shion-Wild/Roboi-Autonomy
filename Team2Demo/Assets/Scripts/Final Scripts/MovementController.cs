using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // Cached References
    PlayerInput playerInput;
    public CharacterController characterController;
    PlayerAbilities playerAbilities;

    // Player Input 
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    public Vector3 cameraRelativeMovement;
    bool isMovementPressed;
    //bool isRunPressed;

    // Constants
    float rotationFactorPerFrame = 15.0f;
    //float dashMultiplier = 21.0f;
    //int zero = 0;

    // Gravity 
    float gravity = -9.8f;
    float groundedGravity = -1f;

    // Jumping
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 8.0f;
    float maxJumpTime = 1.25f;
    bool isJumping = false;

   // Trigger Abilities 
    bool isInvisibilityPressed = false;
    bool isEMPPressed = false;
    bool isDashPressed = false;


    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerAbilities = GetComponent<PlayerAbilities>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Dash.started += OnDash;
        playerInput.CharacterControls.Dash.canceled += OnDash;
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;

        playerInput.CharacterControls.Invisibility.started += OnInvisible;
        playerInput.CharacterControls.Invisibility.canceled += OnInvisible;

        playerInput.CharacterControls.EMP.started += OnEMP;
        playerInput.CharacterControls.EMP.canceled += OnEMP;


        SetupJumpVariables();

    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity * .5f;
            currentRunMovement.y = initialJumpVelocity * .5f;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed);
    }

    /*
    void OnDash(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
        
    }
    */

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        // The change in position that the character should point to

        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = cameraRelativeMovement.z;
        // The current rotation of the character
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // Creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * 7.0f;
        currentMovement.z = currentMovementInput.y * 7.0f;
        //currentRunMovement.x = currentMovementInput.x * dashMultiplier;
        //currentRunMovement.z = currentMovementInput.y * dashMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (characterController.isGrounded) 
        {
            currentMovement.y = groundedGravity;
            //currentRunMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            //currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            //currentRunMovement.y = nextYVelocity;
        }
    }
    void Update()
    {
        HandleRotation();

        cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
        characterController.Move(cameraRelativeMovement * Time.deltaTime);

        /*
        if (isRunPressed)
        {
            cameraRelativeMovement = ConvertToCameraSpace(currentRunMovement);
            characterController.Move(cameraRelativeMovement * Time.deltaTime);
        } else {
            cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
            characterController.Move(cameraRelativeMovement * Time.deltaTime);
        }
        */

        HandleGravity();
        HandleJump();
        HandleInvisibility();
        HandleEMP();
        HandleDash();

    }

    Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        // store the y value of the og vector to rotate 
        float currentYValue = vectorToRotate.y;

        // Get the forward and right directional vectors of the camera 
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Remove the Y values to ignore upward / downward camera angles
        cameraForward.y = 0;
        cameraRight.y = 0;  

        //Re - normalize both vectors so they each have a magnitude of 1
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;   

        // Rotate the x and z vectortoratate values to camera space 
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        // the sum of both products is the vector3 in camera space
        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    // Abililites

    // Invisibility

    void OnInvisible(InputAction.CallbackContext context)
    {
        isInvisibilityPressed = context.ReadValueAsButton();
        Debug.Log(isInvisibilityPressed);
    }

    void HandleInvisibility()
    {
        if (isInvisibilityPressed)
        {
            playerAbilities.TriggerInvisibility();
        }

    }

    // EMP Bomb
    void OnEMP(InputAction.CallbackContext context)
    {
        isEMPPressed = context.ReadValueAsButton();
        Debug.Log(isEMPPressed);
    }

    void HandleEMP()
    {
        if (isEMPPressed)
        {
            playerAbilities.ThrowEMPGrenade();
            isEMPPressed = false;
        }

    }

    // Dash 

    void OnDash(InputAction.CallbackContext context)
    {
        isDashPressed = context.ReadValueAsButton();
        Debug.Log(isDashPressed);
    }

    void HandleDash()
    {
        if (isDashPressed)
        {
            playerAbilities.TriggerDash();
        }

    }


}
