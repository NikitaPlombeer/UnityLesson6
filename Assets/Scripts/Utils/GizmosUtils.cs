using UnityEngine;

namespace Utils
{
    public class GizmosUtils
    {
        public static Vector3 SampleParabola (Vector3 start, Vector3 end, float height, float t)
        {
            float parabolicT = t * 2 - 1;
            if (Mathf.Abs (start.y - end.y) < 0.1f) {
                //start and end are roughly level, pretend they are - simpler solution with less steps
                Vector3 travelDirection = end - start;
                Vector3 result = start + t * travelDirection;
                result.y += (-parabolicT * parabolicT + 1) * height;
                return result;
            } else {
                //start and end are not level, gets more complicated
                Vector3 travelDirection = end - start;
                Vector3 levelDirection = end - new Vector3 (start.x, end.y, start.z);
                Vector3 right = Vector3.Cross (travelDirection, levelDirection);
                Vector3 up = Vector3.Cross (right, travelDirection);
                if (end.y > start.y)
                    up = -up;
                Vector3 result = start + t * travelDirection;
                result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
                return result;
            }
        }
    }
}