using UnityEngine;

/// <summary>
/// From Jasper Flick's Unity Movement Tutorials
/// at catlikecoding.com/unity/tutorials/movement.
/// 
/// Thanks for the guidance Jasper.
/// </summary>
public class MovingSphere : MonoBehaviour {


    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAcceleration = 1f;

    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    [SerializeField, Range(0, 5)]
    int maxAirJumps = 0;

    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 25f, maxStairsAngle = 50f;

    [SerializeField, Range(0f, 100f)]
    float maxSnapSpeed = 100f;

    [SerializeField, Min(0f)]
    float probeDistance = 1f;

    [SerializeField]
    LayerMask probeMask = -1, stairsMask = -1;

    Rigidbody body;

    Vector2 movementInput;
    Vector3 velocity, desiredVelocity;
    Vector3 contactNormal, steepNormal;
    Vector3 xAxis, zAxis;
    Vector3 jumpDirection;

    float maxSpeedChange;
    float jumpSpeed;
    float acceleration;
    float minGroundDotProduct, minStairsDotProduct;
    float alignedSpeed; // Used to calculate speed aligned to surface normal
    float currentX, currentZ;
    float newX, newZ;
    float speed, dot;
    float minDot;

    int groundContactCount, steepContactCount;
    int jumpPhase;
    int stepsSinceLastGrounded, stepsSinceLastJump;

    bool desiredJump;
    bool OnGround => groundContactCount > 0;
    bool OnSteep => steepContactCount > 0;

    /// <summary>
    /// Unity: Function is called when the scripit is loaded or a value is
    /// changed in the inspector (Called in the editor only)
    /// </summary>
    void OnValidate() {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// 
    /// Get our Rigidbody and call OnValidate().
    /// </summary>
    private void Awake() {
        body = GetComponent<Rigidbody>();
        OnValidate();
    }


    /// <summary>
    /// Called every frame.
    /// 
    /// Get our player's movement each frame
    /// </summary>
    void Update() {
        // Get our Sphere's movement input and clamp it to keep our magnitude to a max of 1
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        movementInput = Vector2.ClampMagnitude(movementInput, 1f);

        // Desired velocity calculated with maxSpeed coefficient
        desiredVelocity = new Vector3(movementInput.x, 0f, movementInput.y) * maxSpeed;
        
        // Get our Sphere's Jump Input
        // Input.GetButtonDown is only true for the frame that the button was first pressed
        desiredJump |= Input.GetButtonDown("Jump");


        // Change color if we're on the ground or not
        //GetComponent<Renderer>().material.SetColor("_Color", OnGround ? Color.black : Color.white);

    }

    /// <summary>
    /// Called every fixed framerate frame
    /// </summary>
    void FixedUpdate() {
        UpdateState();
        AdjustVelocity();

        if (desiredJump) {
            desiredJump = false;
            Jump();
        }

        body.velocity = velocity;
        ClearState();

    }

    void UpdateState() {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = body.velocity;

        if (OnGround || SnapToGround() || CheckSteepContacts()) {
            stepsSinceLastGrounded = 0;

            if (stepsSinceLastJump > 1) {
                jumpPhase = 0;
            }

            if (groundContactCount > 1) {
                contactNormal.Normalize();
            }


        } else {
            contactNormal = Vector3.up;
        }

    }

    void Jump() {

        stepsSinceLastJump = 0;
        jumpPhase += 1;

        jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        
        jumpDirection = (jumpDirection + Vector3.up).normalized;
        alignedSpeed = Vector3.Dot(velocity, jumpDirection);

        if (alignedSpeed > 0f) {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }


        if (OnGround) {
            // If we're on the ground

            jumpDirection = contactNormal;

        } else if (OnSteep) {
            // If we're on a steep normal

            jumpDirection = steepNormal;
            jumpPhase = 0;

        } else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps) {
            // IF we can Air Jump and we haven't used all of them (our phases) up

            if (jumpPhase == 0) {
                jumpPhase = 1;
            }

            jumpDirection = contactNormal;

        } else {
            // Don't even bother jumping
            return;
        }

        // Finally, Apply the jump to our velocity
        velocity += jumpDirection * jumpSpeed;

    }

    void OnCollisionEnter(Collision collision) {
        EvaluateCollision(collision);
    }

    void OnCollisionStay(Collision collision) {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision collision) {

        minDot = GetMinDot(collision.gameObject.layer);

        for (int i = 0; i < collision.contactCount; i++) {
            Vector3 normal = collision.GetContact(i).normal;

            if (normal.y > minDot) {
                groundContactCount += 1;
                contactNormal = normal;
            } else if (normal.y > -0.01f) {
                steepContactCount += 1;
                steepNormal += normal;
            }

        }

    }

    Vector3 ProjectOnContactPlane(Vector3 vector) {
        return vector - contactNormal * Vector3.Dot(vector, contactNormal);
    }

    void AdjustVelocity() {
        xAxis = ProjectOnContactPlane(Vector3.right).normalized;
        zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

        currentX = Vector3.Dot(velocity, xAxis);
        currentZ = Vector3.Dot(velocity, zAxis);

        acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;

        newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
        newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);

    }

    void ClearState() {
        groundContactCount = steepContactCount = 0;
        contactNormal = steepNormal = Vector3.zero;
    }

    bool SnapToGround() {

        // Abort if it's too soon after a jump, 2 steps should work
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2) {
            return false;
        }


        speed = velocity.magnitude;
        if (speed > maxSnapSpeed) {
            return false;
        }

        if (!Physics.Raycast(body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask)) {
            return false;
        }

        if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer)) {
            return false;
        }


        groundContactCount = 1;
        contactNormal = hit.normal;
        dot = Vector3.Dot(velocity, hit.normal);

        if (dot > 0f) {
            velocity = (velocity = hit.normal * dot).normalized * speed;
        }


        return true;
    }

    float GetMinDot(int layer) {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }

    bool CheckSteepContacts() {
        if (steepContactCount > 1) {
            steepNormal.Normalize();

            if (steepNormal.y >= minGroundDotProduct) {
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }

        }

        return false;
    }

}