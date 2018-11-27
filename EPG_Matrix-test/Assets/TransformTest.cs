using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour {
    public Transform targetTransform;

    private Vector3 rotatedForward;
    private Vector3 rotatedUp;

    void Start ()
    {

    }
	
	void Update ()
    {
        Vector3 targetRotetion = targetTransform.eulerAngles;
        Matrix3x3 rotationMatrix = Matrix3x3.Euler(targetRotetion);
        rotatedForward = rotationMatrix * Vector3.forward;
        rotatedUp = rotationMatrix * Vector3.up;
        transform.LookAt(rotatedForward + this.transform.position, rotatedUp);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, rotatedForward * 1.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, rotatedUp * 1.5f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, rotatedForward * 1.5f + transform.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, rotatedUp * 1.5f + transform.position);


    }
    private void DrawRotationTrail(float angle, AxisId axis, Vector3 start)
    {
        Vector3 trail = start;
        for (int i = 0; i <= 16; i++) {
            Vector3 trailNextStep = Matrix3x3.Rotate(angle%360*i/16f, axis) * start;
            Gizmos.DrawLine(trail, trailNextStep);
            trail = trailNextStep;
        }
    }
}
