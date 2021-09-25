using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public float FollowSpeed;
    public bool Immediate = false;
    
    void Update()
    {
        if (Immediate)
        {
            ImmediatelyMove();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * FollowSpeed);
        }
    }

    public void ImmediatelyMove()
    {
        transform.position = Target.position;
    }
}
