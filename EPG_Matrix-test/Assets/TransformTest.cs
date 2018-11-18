using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour {
    public Transform targetTransform;
    public float speed = 10;
    public float alphaX = 0 , betaY =0, gammaZ =0;
    Vector3 rotatedVectorUpAroundX, rotatedVectorForwardAroundY, rotatedVectorRightAroundZ;
    private Vector3 fromThisToTarget;
    private Vector3 rotatedForward;
    private Vector3 rotatedUp;

    void Start ()
    {
        /*
        rotatedVectorUpAroundX = Vector3.up;
        Matrix3x3 rotationMatrixX = Matrix3x3.Rotate(90, AxisId.Right);
        Debug.Log("rotationMatrixX:\n" + rotationMatrixX);
        Debug.Log("(1,2,3) rotate: 90, axis: X: " +  rotationMatrixX *new Vector3(1,2,3));
        Matrix3x3 rotationMatrixY = Matrix3x3.Rotate(90, AxisId.Up);
        Debug.Log("(1,0,0) rotate: 90, axis: Y: " + rotationMatrixY * Vector3.right);
        Matrix3x3 rotationMatrixZ = Matrix3x3.Rotate(90, AxisId.Forward);
        Debug.Log("(1,0,0) rotate: 90, axis: Z: " + rotationMatrixZ * Vector3.right);
        float[] m1 = {2,0,0,0,3,0,0,0,1 };
        Matrix3x3 matrx1 = new Matrix3x3(m1);
        float[] m2 = { 1, 0, 4, 0, 1, 5, 0, 0, 1 };
        Matrix3x3 matrx2 = new Matrix3x3(m2);
        Debug.Log("matrx1*matrx2: " + matrx1 * matrx2);
        */
    }
	
	// Update is called once per frame
	void Update ()
    {/*
        fromThisToTarget = targetTransform.position - this.transform.position;
        this.transform.position += fromThisToTarget.normalized * Time.deltaTime * speed;
        this.transform.LookAt(targetTransform.position);*/

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

        /*
        Matrix3x3 rotationMatrixX = Matrix3x3.Rotate(alphaX, AxisId.Right);
        rotatedVectorUpAroundX = rotationMatrixX * Vector3.up;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, rotatedVectorUpAroundX);
        DrawRotationTrail(alphaX, AxisId.Right, Vector3.up);

        Matrix3x3 rotationMatrixY = Matrix3x3.Rotate(betaY, AxisId.Up);
        rotatedVectorForwardAroundY = rotationMatrixY * Vector3.forward;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, rotatedVectorForwardAroundY);
        DrawRotationTrail(betaY, AxisId.Up, Vector3.forward);

        Matrix3x3 rotationMatrixZ = Matrix3x3.Rotate(gammaZ, AxisId.Forward);
        rotatedVectorRightAroundZ = rotationMatrixZ * Vector3.right;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, rotatedVectorRightAroundZ);
        DrawRotationTrail(gammaZ, AxisId.Forward, Vector3.right);
        */


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
