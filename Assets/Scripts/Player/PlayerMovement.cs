using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls playerControls;
    private InputAction move, look, jump, sprintStart, sprintEnd;
    
    private Rigidbody rb;
    private Camera cam;
    private SphereCollider groundCheckerCollider;
    private Collider[] colliders;

    Vector3 moveInput;
    Vector3 moveDirection;
    Vector3 verticalMove;
    Vector3 lookDirection;

    public float moveSpeed = 10f;
    public float sprintSpeedMultiplier = 1.5f;
    public float maxSprintTime = 7f;
    public float sprintCooldown = 3f;
    public float jumpForce = 10f;
    public float gravityScale = 5;
    public float aerialMoveSpeedMultiplier = 0.75f; 
    public float groundCheckDistance = 0.2f;

    private Vector3 velocityWhenJumped;

    private float sprintTime = 5f;
    private float sprintCooldownTime = 3f;

    private float upperLookLimit = 60f;
    private float lowerLookLimit = -60f;
    private float rotationX;
    private float rotationY;

    private Vector3 groundCheckerLocation;
    private float groundCheckerSize;

    public float maxSlopeAngle = 40f;
    public float slopeDetectRayHeight = 0.2f;
    private RaycastHit slopeHit;
    private bool isGrounded;
    private bool isJumpable;
    private bool exitingSlope;
    private bool isSprinting = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        look = playerControls.Player.Look;
        look.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        sprintStart = playerControls.Player.SprintStart;
        sprintStart.Enable();
        sprintStart.performed += SprintStart;

        sprintEnd = playerControls.Player.SprintFinish;
        sprintEnd.Enable();
        sprintEnd.performed += SprintEnd;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        sprintStart.Disable();
        sprintEnd.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        groundCheckerCollider = GetComponentInChildren<SphereCollider>();
    }

    void Update()
    {
        Look();
        SprintTimer();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
    }
    private void Look()
    {
        
        lookDirection = look.ReadValue<Vector2>();

        rotationY += /*lookSensitivity **/ lookDirection.x;
        rotationX -= /*lookSensitivity **/ lookDirection.y;

        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit);

        cam.transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0f);
        transform.eulerAngles = new Vector3(0f, rotationY, 0f);
    }

    private void Move()
    {
        moveInput = move.ReadValue<Vector2>();
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        Vector3 down = Vector3.down;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = moveInput.y * forward + moveInput.x * right;

        //this is the direction in the world space we want to move:
        Vector3 horizontalMove;
        verticalMove = new Vector3(0f, rb.velocity.y, 0f);

        if (OnSlope() && isGrounded)
        {
            print("player is on slope");
            if (isSprinting)
            {
                rb.velocity = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal) * moveSpeed * sprintSpeedMultiplier /*+ verticalMove*/;
            }
            else
            {
                rb.velocity = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal) * moveSpeed /*+ verticalMove*/;
            }
        }
        
        else if (isGrounded && isSprinting)
        {
            horizontalMove = moveSpeed * sprintSpeedMultiplier * moveDirection;
            rb.velocity = horizontalMove /*+ verticalMove*/;
        }

        else if (isGrounded && !isSprinting)
        {
            horizontalMove = moveSpeed * moveDirection;
            rb.velocity = horizontalMove /*+ verticalMove*/;
        }

        else if (!isGrounded)
        {                
            horizontalMove = moveSpeed * aerialMoveSpeedMultiplier * moveDirection;            
            rb.velocity = horizontalMove + velocityWhenJumped /*+ verticalMove*/;
            rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
            print(rb.velocity.y);
        }
         
    }

    private bool OnSlope()
    {
        float playerRadius = GetComponent<CapsuleCollider>().radius;
        for (int i = 0; i < 24; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / 24 * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, slopeDetectRayHeight, vertical);

            /* Get the spawn position */
            var sideOfPlayer = transform.position + spawnDir * playerRadius; // Radius is just the distance away from the point

            //Debug.DrawRay(sideOfPlayer, Vector3.down, Color.green, 10);
            if (Physics.Raycast( sideOfPlayer, Vector3.down, out slopeHit, 0.1f))
            {
                Debug.DrawRay(sideOfPlayer, Vector3.down, Color.green, 10);
                print("detecting floor");
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                if ( angle < maxSlopeAngle && angle != 0)
                {
                    return true;
                }
            } 
        }
        
        return false;        
    }

    /*private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }*/

    private void CheckGround()
    { 
        groundCheckerLocation = groundCheckerCollider.transform.position + new Vector3(0f, 0.39f, 0f);
        groundCheckerSize = groundCheckerCollider.radius * 1.1f;

        isGrounded = false;
        //isJumpable = false;
        int layerMask = LayerMask.GetMask("Ground");

        colliders = Physics.OverlapSphere(groundCheckerLocation, groundCheckerSize, layerMask);

        if (colliders.Length > 0)
        {
            isGrounded = true;
            exitingSlope = false;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        exitingSlope = true;
        if (isGrounded)
        {
            print("Jumped");
            
            Vector3 force = new Vector3(0.0f, jumpForce, 0.0f);
            
            rb.AddForce(force, ForceMode.Impulse);
            velocityWhenJumped = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private void SprintTimer()
    {
        if (sprintTime > 0 && isSprinting)
        {
            sprintTime -= Time.deltaTime;
            sprintCooldownTime = sprintCooldown * sprintTime/maxSprintTime;
            sprintCooldownTime = Mathf.Round(sprintCooldownTime * 100f) * 0.01f;

            if (sprintTime <= 0)
            {
                isSprinting = false;
            }
        }
        
        else if (!isSprinting)
        {
            if (sprintCooldownTime < sprintCooldown)
            {
                
                sprintCooldownTime += Time.deltaTime;
                sprintCooldownTime = Mathf.Round(sprintCooldownTime * 100f) * 0.01f;

                if (sprintTime < maxSprintTime)
                {
                    sprintTime = maxSprintTime * sprintCooldownTime/sprintCooldown;
                    sprintTime = Mathf.Round(sprintTime * 100f) * 0.01f;
                }
            }
        }

    }

    private void SprintStart(InputAction.CallbackContext context)
    {
        if (sprintCooldownTime > 0f)
        {
            isSprinting = true;
        }
        
    }

    private void SprintEnd(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckerLocation, groundCheckerSize);
    }
}
