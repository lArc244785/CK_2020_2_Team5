using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Configuration;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    public enum CamType{
        None, Target
    }

    private CamType camState;

    public Transform targetTr;

    private Transform camTr;
    private Transform camPiovtRoation;


    [Range(0.0f, 1.0f)]
    public float smoothSpeed = 5.0f;
    private Vector3 TargetPoint;
    private Vector3 currentCamPoint;
    private Vector3 currentCamPointVelocity;

    private Vector2 upperLeftPoint;
    private Vector2 downRightPoint;

    [Range(5.0f, 30.0f)]
    public float distance = 15.0f;

    [Range(30, 60)]
    public float camAngle = 45.0f;


    private void Start()
    {
        camTr = transform;
        camPiovtRoation = transform.GetChild(0);
        currentCamPoint = Vector3.zero;
        setCamType(CamType.Target);
    }

    private void Update()
    {
        camPiovtRoation.localRotation = Quaternion.Euler(camAngle, 0, 0);
        switch (camState)
        {
            case CamType.Target:
                TargetCam();
                break;
        }
    }


   public void setCamType(CamType type)
    {
        camState = type;
    }

    private void TargetCam()
    {
        DistancePoint();

        print("CODE44: " + upperLeftPoint.x +  downRightPoint.x);

        TargetPoint = new Vector3(
            Mathf.Clamp(TargetPoint.x, upperLeftPoint.x, downRightPoint.x),
            TargetPoint.y,
            Mathf.Clamp(TargetPoint.z, downRightPoint.y, upperLeftPoint.y));



        if (currentCamPoint == Vector3.zero)
        {
            currentCamPoint = TargetPoint;
        }

            currentCamPoint = Vector3.SmoothDamp(currentCamPoint, TargetPoint, ref currentCamPointVelocity, smoothSpeed ) ;

        camTr.position = currentCamPoint;
    }

    private void DistancePoint()
    {
        float radian = camAngle * Mathf.PI / 180.0f;

        float z =  targetTr.position.z - Mathf.Cos(radian) * distance;
        float y = targetTr.position.y +Mathf.Sin(radian) * distance;

        TargetPoint = new Vector3(targetTr.position.x, y, z);
        print("TargetPoint   " + TargetPoint);
    }

    public void SetOffset(Vector2 upperLeft, Vector2 downRight)
    {
        upperLeftPoint = upperLeft;
        downRightPoint = downRight;
    }

}
