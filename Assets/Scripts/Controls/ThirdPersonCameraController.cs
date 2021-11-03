using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offsetPos = new Vector3(5f, 5f, 0f);
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;
    public float smoothSpeed = 0.5f;

    Quaternion targetRotation;
    Vector3 targetPos;
    //bool smoothRotating;

    // Update is called once per frame
    void Update() {

        MoveWithTarget();
        LookAtTarget();

        // If get LeftRotateInputKey && !smoothRotating
        //      StartCoroutine("RotateAroundTarget", 45);
        // If get Right...
        //      Start(... -45);


    }

    /// <summary>
    /// Move the camera to the player position + current camera offset
    /// Offset is modified by the RotateAroundTarget coroutine
    /// </summary>
    void MoveWithTarget() {
        targetPos = target.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void LookAtTarget() {
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// A coroutine
    /// Determined by 'smoothRotating'
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    IEnumerator RotateAroundTarget(float angle) {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos = Quaternion.Euler(0, angle, 0) * offsetPos;
        float dist = Vector3.Distance(offsetPos, targetOffsetPos);

        // 0.02f is arbitrary
        while(dist > 0.02f) {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetOffsetPos, ref vel, smoothSpeed);
            dist = Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;

        }

        //smoothRotating = false;
        offsetPos = targetOffsetPos;

    }

}
