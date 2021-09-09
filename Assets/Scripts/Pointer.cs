using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform Aim;
    public Camera PlayerCamera;

    void Update()
    {
        Vector3 mouseWorldPosition = MouseUtils.GetMouseIntersectionWithPlane();
        Aim.position = mouseWorldPosition;

        Vector3 toAim = mouseWorldPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(toAim);
    }


}