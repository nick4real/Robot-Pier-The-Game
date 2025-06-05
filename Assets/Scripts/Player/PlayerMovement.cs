using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Keybinds")]
    private Rigidbody rigidBody;

    [Header("Movement")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 0.25f;
    [SerializeField] private float airMultiplier = 1f;
    private bool readyToJump = true;
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    private Vector3 moveDirection;

    [Header("Ground Check")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    [Header("Slope Handling")]
    [SerializeField] public float maxSlopeAngle;
    private RaycastHit slopeHit;
    
    // Default Methods
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        SpeedControl();

        if (grounded) rigidBody.drag = groundDrag;
        else rigidBody.drag = 0;
    }

    void FixedUpdate() => MovePlayer();
    
    // Input System
    private void OnMove(InputValue input)
    {
        var temp = input.Get<Vector2>();
        Debug.Log("On move " + temp);
        horizontalInput = temp.x;
        verticalInput = temp.y;
    }

    private void OnJump(InputValue input)
    {
        if (input.isPressed && grounded && readyToJump)
        {
            Debug.Log("On jump " + input);
            Jump();
        }
    }

    // Other
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope())
            rigidBody.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 10f, ForceMode.Force);

        if (grounded)
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    void SpeedControl()
    {
        if (OnSlope() && rigidBody.velocity.magnitude > moveSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
            return;
        }

        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limVel.x, rigidBody.velocity.y, limVel.z);
        }
    }

    void Jump()
    {
        readyToJump = false;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    void ResetJump() => readyToJump = true;

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 md)
    {
        return Vector3.ProjectOnPlane(md, slopeHit.normal).normalized;
    }
}
