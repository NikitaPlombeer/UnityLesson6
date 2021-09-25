using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
    Left, Right
}
public class Walker : MonoBehaviour
{

    public Transform LeftTarget;
    public Transform RightTarget;
    public Transform RayStart;
    
    public float Speed = 1;

    public float StopTime = 0.5f;

    public Direction CurrentDirection;

    public UnityEvent EventOnLeftTarget;
    public UnityEvent EventOnRightTarget;
    
    private bool _isStopped = false;
    
    void Start()
    {
        LeftTarget.parent = null;
        RightTarget.parent = null;
    }

    void Update()
    {
        if (_isStopped) return;
        
        Vector3 translatePosition;
        if (CurrentDirection == Direction.Left)
        {
            translatePosition = new Vector3(-Time.deltaTime * Speed, 0f, 0f);
            if (transform.position.x < LeftTarget.position.x)
            {
                CurrentDirection = Direction.Right;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);
                EventOnLeftTarget.Invoke();
            }
        }
        else
        {
            translatePosition = new Vector3(Time.deltaTime * Speed, 0f, 0f);
            if (transform.position.x > RightTarget.position.x)
            {
                CurrentDirection = Direction.Left;
                _isStopped = true;
                Invoke("ContinueWalk", StopTime);
                EventOnRightTarget.Invoke();
            }
        }

        transform.position += translatePosition;

        RaycastHit hit;
        if (Physics.Raycast(RayStart.position, Vector3.down, out hit))
        {
            transform.position = hit.point;
        }
    }

    void ContinueWalk()
    {
        _isStopped = false;
    }
}
