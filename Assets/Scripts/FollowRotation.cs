using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public Transform Target;
    
    private void Update()
    {
        transform.rotation = Target.rotation;
    }
}