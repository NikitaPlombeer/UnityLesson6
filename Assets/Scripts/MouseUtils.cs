using UnityEngine;

public class MouseUtils
{
    
    public static Vector3 GetMouseIntersectionWithPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane zPlane = new Plane(-Vector3.forward, Vector3.zero);
        zPlane.Raycast(ray, out var distance);
        return ray.GetPoint(distance);
    }
    
}