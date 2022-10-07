using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 8;
    public float acceleration = 50;
    public float jumpStrength = 8;
    public float jumpRedirectionAcceleration = -100;
    public float jumpQueueAllowance = 0.2f;
    public float coyoteTimeAllowance = 0.2f;
    public float gravityForce = 50;
    public Transform model;
    public Transform cameraTarget;
    public BowController bow;

    private Rigidbody rb;
    private bool jumping;

    private float timeSinceJumpPressed;
    private float timeSinceLeftPlatform;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Vector3 forwardTarget = Vector3.zero;
        if (bow.GetState() == BowController.State.None)
        {
            // if (GetHorizontalVelocity().magnitude > 0.1)
            forwardTarget = GetHorizontalVelocity();
            // else
            // forwardTarget = transform.forward;
        }
        else
        {
            forwardTarget = Camera.main.transform.forward;
        }
        forwardTarget.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, forwardTarget, Time.deltaTime * 8);

        // Update timeSinceLeftPlatform.
        if (IsGrounded())
        {
            jumping = false;
            timeSinceLeftPlatform = 0;
        }
        timeSinceLeftPlatform += Time.deltaTime;

    }

    void FixedUpdate()
    {
        // Handle lateral movement.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);
        inputVector = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * inputVector;

        AccelerateToLateralVelocity(inputVector * baseSpeed, acceleration);

        animator.SetFloat("Speed", GetHorizontalVelocity().magnitude / baseSpeed);
        animator.SetBool("Aiming", bow.GetState() != BowController.State.None);

        // Check for a jump, taking into account jump queueing.
        if (Input.GetButtonDown("Jump"))
        {
            timeSinceJumpPressed = 0;
        }
        timeSinceJumpPressed += Time.deltaTime;

        if (!jumping && timeSinceLeftPlatform < coyoteTimeAllowance && timeSinceJumpPressed < jumpQueueAllowance)
        {
            jumping = true;
            SetVerticalVelocity(jumpStrength);
            animator.SetTrigger("Jump");
        }

        // Add a downwards force if jumping and not holding the jump button.
        if (!IsGrounded() && rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.AddForce(Vector3.down * jumpRedirectionAcceleration);
        }

        rb.AddForce(Vector3.down * gravityForce * Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, 0.2f);
        animator.SetBool("IsGrounded", grounded);
        return grounded;
    }

    Vector3 GetHorizontalVelocity()
    {
        Vector3 vel = rb.velocity;
        vel.y = 0;
        return vel;
    }

    void SetVerticalVelocity(float value)
    {
        Vector3 vel = rb.velocity;
        vel.y = value;
        rb.velocity = vel;
    }

    void AccelerateToLateralVelocity(Vector3 targetVelocity, float acceleration)
    {
        Vector3 vel = rb.velocity;
        float cachedY = vel.y;
        vel.y = 0;
        vel = Vector3.MoveTowards(vel, targetVelocity, Time.fixedDeltaTime * acceleration);
        vel.y = cachedY;
        rb.velocity = vel;
    }
}
