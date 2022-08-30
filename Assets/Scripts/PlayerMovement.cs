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
    
    private Rigidbody rb;
    private Camera cam;

    Vector3 moveDirection;
    Vector3 lookDirection;

    public float moveSpeed = 10f;
    public float lookSensitivity = 10f;
    public float upperLookLimit = 30f;
    public float lowerLookLimit = -30;
    private float rotationX;
    private float rotationY;

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
    }

    void Update()
    {
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Look()
    {
        moveDirection = move.ReadValue<Vector2>();
        lookDirection = look.ReadValue<Vector2>();

        rotationY += lookSensitivity * lookDirection.x;
        rotationX -= lookSensitivity * lookDirection.y;

        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit);

        cam.transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0f);
        transform.eulerAngles = new Vector3(0f, rotationY, 0f);
    }

    private void Move()
    {
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * moveDirection.y + right * moveDirection.x;

        rb.velocity = desiredMoveDirection * moveSpeed * Time.deltaTime;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        print("Fired");
    }
}
