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
    DashCooldown dashCooldown;
    EMPCooldown empCooldown;
    InvisibilityCooldown invisibilityCooldown;

    // Player Input 
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    public Vector3 cameraRelativeMovement;
    bool isMovementPressed;
    public float moveSpeed = 10.0f;
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
    float maxJumpTime = 1.2f;
    bool isJumping = false;

    // Trigger Abilities 
    bool isInvisibilityPressed = false;
    bool isEMPPressed = false;
    bool isDashPressed = false;

    // Ability SFX
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip emp;


    // Static Variables
    public static bool isEMPActivated = true;
    public static bool isInvisibilityActivated = true;


    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerAbilities = GetComponent<PlayerAbilities>();

        dashCooldown = GameObject.Find("DashCooldown").GetComponent<DashCooldown>();
        empCooldown = GameObject.Find("EMPCooldown").GetComponent<EMPCooldown>();
        invisibilityCooldown = GameObject.Find("InvisibilityCooldown").GetComponent<InvisibilityCooldown>();

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

    void Start()
    {
        SoundManager.Instance.PlayBackgroundMusic();
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    void Update()
    {
        HandleRotation();

        cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
        characterController.Move(cameraRelativeMovement * Time.deltaTime);

        HandleGravity();
        HandleJump();
        HandleInvisibility();
        HandleEMP();
        HandleDash();
    }

    //Abilities 
    // Dash 
    void HandleDash()
    {
        if (isDashPressed)
        {
            playerAbilities.TriggerDash();
            SoundManager.Instance.PlayCharacterSound(dash);
            dashCooldown.TriggerAbility();
        }
    }
    // EMP Bomb
    void HandleEMP()
    {
        if (isEMPPressed && isEMPActivated)
        {
            playerAbilities.ThrowEMPGrenade();
            isEMPPressed = false;
            SoundManager.Instance.PlayCharacterSound(emp);
            empCooldown.TriggerAbility();
        }
    }
    // Invisibility
    void HandleInvisibility()
    {
        if (isInvisibilityPressed && isInvisibilityActivated)
        {
            playerAbilities.TriggerInvisibility();
            invisibilityCooldown.TriggerAbility();
        }
    }

    /// <summary>
    /// Base Movement
    /// </summary>

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
            currentMovement.y = initialJumpVelocity * .7f;
            currentRunMovement.y = initialJumpVelocity * .7f;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

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
        currentMovement.x = currentMovementInput.x * moveSpeed;
        currentMovement.z = currentMovementInput.y * moveSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 1.7f;

        if (characterController.isGrounded) 
        {
            currentMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
        }
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
  
    

    /// <summary>
    /// Capture Input
    /// </summary>
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    void OnInvisible(InputAction.CallbackContext context)
    {
        isInvisibilityPressed = context.ReadValueAsButton();
    }
    void OnEMP(InputAction.CallbackContext context)
    {
        isEMPPressed = context.ReadValueAsButton();
    }
    void OnDash(InputAction.CallbackContext context)
    {
        isDashPressed = context.ReadValueAsButton();
    }

}
