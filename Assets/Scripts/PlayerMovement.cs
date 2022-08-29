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
    public float upperLookLimit = -65f;
    public float lowerLookLimit = 70f;
    private float verticalRotation;

    bool canLook = true;

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
        moveDirection = move.ReadValue<Vector2>();
        lookDirection = look.ReadValue<Vector2>();
    }

    private void FixedUpdate()
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
        transform.rotation *= Quaternion.Euler(0, lookDirection.x * lookSensitivity * Time.deltaTime, 0);

        verticalRotation = Mathf.Clamp(-lookDirection.y, lowerLookLimit, upperLookLimit) * lookSensitivity * Time.deltaTime;
        
        cam.transform.rotation *= Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        print("Fired");
    }
}
