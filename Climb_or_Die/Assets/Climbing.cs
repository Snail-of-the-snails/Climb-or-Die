using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("Refrences")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    bool wallInFrontOfPlayer;

    void Update()
    {
        WallCheck();
    }

    private void StateMachine() {
        if (wallInFrontOfPlayer && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle) {
            if (!climbing && climbTimer > 0) {
                StartClimbing();
            }

            if (climbTimer > 0) {
                climbTimer -= Time.deltaTime;
            }

            if (climbTimer < 0) {
                StopClimbing();
            }
        }
        else {
            if (climbing) {
                StopClimbing();
            }
        }
    }

    private void WallCheck() {
        wallInFrontOfPlayer = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);
    }

    private void StartClimbing()
    {
        climbing = true;
    }

    private void ClimbingMovement() {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, climbSpeed, rb.linearVelocity.z);
    }

    private void StopClimbing() {
        climbing = false;
    }
}
