using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public PlayerControls playerControls;
    private InputAction move;
    private InputAction look;
    private InputAction fire;
    private InputAction jump;
    
    private Rigidbody rb;
    private Camera cam;
    private SphereCollider groundCheckerCollider;
    private Collider[] colliders;

    Vector3 moveDirection;
    Vector3 lookDirection;

    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float lookSensitivity = 10f;
    public float groundCheckDistance = 0.2f;
    
    private float upperLookLimit = 60f;
    private float lowerLookLimit = -60f;
    private float rotationX;
    private float rotationY;

    private Vector3 groundCheckerLocation;
    private float groundCheckerSize;

    private bool isGrounded;

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

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        fire.Disable();
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
    }

    private void FixedUpdate()
    {
        Move();
        
        groundCheckerLocation = groundCheckerCollider.transform.position + new Vector3(0f, 0.39f, 0f);
        groundCheckerSize = groundCheckerCollider.radius * 1.1f;
        CheckGround();
    }
    private void Look()
    {
        
        lookDirection = look.ReadValue<Vector2>();

        rotationY += lookSensitivity * lookDirection.x;
        rotationX -= lookSensitivity * lookDirection.y;

        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit);

        cam.transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0f);
        transform.eulerAngles = new Vector3(0f, rotationY, 0f);
    }

    private void Move()
    {
        moveDirection = move.ReadValue<Vector2>();
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        Vector3 down = -transform.up;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();
        down.Normalize();

        //this is the direction in the world space we want to move:
        Vector3 horizontalMove = (moveDirection.y * forward + moveDirection.x * right) * moveSpeed;
        Vector3 veritcalMove = new Vector3(0f, rb.velocity.y, 0f) + down;

        rb.velocity =  horizontalMove + veritcalMove;
    }


    private void CheckGround()
    {
        isGrounded = false;
        int layerMask = LayerMask.GetMask("Ground");

        colliders = Physics.OverlapSphere(groundCheckerLocation, groundCheckerSize, layerMask);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckerLocation, groundCheckerSize);
    }
    

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            print("Jumped");
            Vector3 force = new Vector3(0.0f, jumpForce, 0.0f);
            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        print("Fired");
    }
}
