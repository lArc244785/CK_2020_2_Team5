using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Configuration;
using UnityEngine;

public class CameraManger : MonoBehaviour
{


    private EnumInfo.CamType camState;

    public Transform targetTr;

    private Transform camTr;
    private Transform camPiovtRoation;

    private Camera camera;

    [Range(0.0f, 1.0f)]
    public float smoothTime = 0.125f;
    private Vector3 TargetPoint;
    private Vector3 currentCamPoint;

    private Vector2 upperLeftPoint;
    private Vector2 downRightPoint;

    [Range(5.0f, 100.0f)]
    public float distance = 15.0f;

    [Range(30, 90)]
    public float camAngle = 45.0f;

    private Vector3 fixingPoint;

    public void Setting()
    {
        camTr = transform;
        camPiovtRoation = transform.GetChild(0);
        currentCamPoint = Vector3.zero;
        setCamType(EnumInfo.CamType.Target);
        camera = transform.GetChild(0).GetComponentInChildren<Camera>();
    }


    

    private void FixedUpdate()
    {
        camPiovtRoation.localRotation = Quaternion.Euler(camAngle, 0, 0);
        switch (camState)
        {
            case EnumInfo.CamType.Target:
                TargetCam();
                break;
            case EnumInfo.CamType.Fixing:
                FixingCam();
                break;
        }
    }

    public void SetFixingCameraPoint(Vector3 position)
    {

        fixingPoint = position;
    }

    public void FixingCam()
    {
        float radian = camAngle * Mathf.PI / 180.0f;

        float y = targetTr.position.y + Mathf.Sin(radian) * distance;

        camTr.position = new Vector3(fixingPoint.x, y, fixingPoint.z);


    }

   public void setCamType(EnumInfo.CamType  type)
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
        else
        {
            currentCamPoint = Vector3.Lerp(currentCamPoint, TargetPoint, smoothTime);
        }
        Debug.Log("Code 445: " + currentCamPoint.x + "   " + currentCamPoint.z);
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

    public Camera getCamera()
    {
        return camera;
    }

}
