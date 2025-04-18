using UnityEngine;
using UnityEngine.Video;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float movementSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float normalJumpForce;
    private float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Jumping")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private MovementState state;
    private enum MovementState {
        walking,
        sprinting,
        crouching,
        air
    }

    [Header("Camera")]
    public Camera cam;
    public float normalFOV;
    public float sprintFOV;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight * 0.5f) + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded) {
            rb.linearDamping = groundDrag; 
        }
        else {
            rb.linearDamping = 0;
        }
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler() {
        
        if (Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            movementSpeed = crouchSpeed;
            jumpForce = 0f;
            cam.fieldOfView = normalFOV;
        }
        else if (grounded && Input.GetKey(sprintKey) && state != MovementState.crouching) {
            state = MovementState.sprinting;
            movementSpeed = sprintSpeed;
            jumpForce = normalJumpForce;
            cam.fieldOfView = sprintFOV;
        }
        else if (grounded && state != MovementState.crouching) {
            state = MovementState.walking;
            movementSpeed = walkSpeed;
            jumpForce = normalJumpForce;
            cam.fieldOfView = normalFOV;
        }
        else {
            state = MovementState.air;
        }
    }

    private void MovePlayer() {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (OnSlope()) {
            rb.AddForce(GetSlopeMoveDirection() * movementSpeed * 10f, ForceMode.Force);
        }
        else if (grounded) {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) {
            rb.AddForce(movementDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > movementSpeed) {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void ResetJump() {
        readyToJump = true;
    }

    private bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (playerHeight * 0.5f) + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection() {
        return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    }
}
