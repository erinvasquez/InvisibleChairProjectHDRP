using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 1. 8-Directional movement
/// 2. stop and face current direction when input is absent
/// </summary>
public class ThirdPersonMovementController : MonoBehaviour {

    public float velocity = 5.0f;
    public float turnSpeed = 10.0f;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    public Transform cam;

    // Start is called before the first frame update
    void Start() {
        //cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {

        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
        Rotate();
        Move();


    }

    /// <summary>
    /// Input based on Horizontal (A,D) and Vertical (W,S) keys
    /// </summary>
    void GetInput() {
        //input.x = 
        //input.y 
    }

    public void OnForward(InputAction.CallbackContext context) {
        input.y = context.ReadValue<float>();
    }

    public void OnLeft(InputAction.CallbackContext context) {
        input.x = -1f * context.ReadValue<float>();
    }

    public void OnRight(InputAction.CallbackContext context) {
        input.x = context.ReadValue<float>();
    }

    public void OnBackward(InputAction.CallbackContext context) {
        input.y = -1f * context.ReadValue<float>();
    }


    void CalculateDirection() {
        angle = Mathf.Atan2(input.x, input.y);
        angle *= Mathf.Rad2Deg;

        angle += cam.eulerAngles.y;

    }

    /// <summary>
    /// Rotate towardd calculated angle
    /// </summary>
    void Rotate() {
        targetRotation = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move() {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

}
