using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Transform Aim;

    void LateUpdate()
    {
        Vector3 mouseWorldPosition = MouseUtils.GetMouseIntersectionWithPlane();
        Aim.position = mouseWorldPosition;

        Vector3 toAim = mouseWorldPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(toAim);
    }


}