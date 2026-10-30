using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField] float gravity = -10f;

    [Header("Dash")]
    [SerializeField] float dashSpeed = 14f;
    [SerializeField] float dashDuration = 0.18f;
    [SerializeField] float dashCooldown = 0.6f;

    [Header("Input Actions")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference jumpAction;
    [SerializeField] InputActionReference dashAction;


    [SerializeField] Transform cameraTransform;


    CharacterController controller;

    Vector2 moveInput;
    Vector3 velocity;
    Vector3 dashDirection;

    bool isGrounded;
    bool jumpQueued;
    bool dashQueued;
    bool isDashing;

    float dashTimer;
    float dashCooldownTimer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        dashAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        dashAction.action.Disable();
    }

    void Update()
    {
        ReadInput();
        UpdateGroundedState();
        HandleJump();
        UpdateDashState();
        ApplyGravity();
        ApplyMovement();
        ApplyMovementCamera();
    }

    void ReadInput()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();

        if (jumpAction.action.WasPressedThisFrame())
        {
            jumpQueued = true;
        }

        if (dashAction.action.WasPressedThisFrame())
        {
            dashQueued = true;
        }
    }

    void UpdateGroundedState()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    void HandleJump()
    {
        if (!jumpQueued)
            return;

        jumpQueued = false;

        if (!isGrounded)
            return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void UpdateDashState()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }

            return;
        }

        if (!dashQueued || dashCooldownTimer > 0f)
        {
            dashQueued = false;
            return;
        }

        dashQueued = false;

        Vector3 inputDir = transform.right * moveInput.x + transform.forward * moveInput.y;

        dashDirection = inputDir.sqrMagnitude > 0.01f? 
        inputDir.normalized: transform.forward;

        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
    }

    void ApplyGravity()
    {
        if (!isGrounded || velocity.y > 0f)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    void ApplyMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        Vector3 horizontal = isDashing? 
        dashDirection * dashSpeed: move * moveSpeed;

        Vector3 finalMove = horizontal + Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }

    void ApplyMovementCamera()
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 move =
            cameraRight * moveInput.x +
            cameraForward * moveInput.y;

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        Vector3 horizontal =
            isDashing
            ? move.normalized * dashSpeed
            : move * moveSpeed;

        Vector3 finalMove =
            horizontal +
            Vector3.up * velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }
}
