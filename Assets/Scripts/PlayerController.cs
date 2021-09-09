using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public Transform Body;
    public Transform ColliderTransform;
    public float MoveSpeed = 5;
    public float JumpHeight = 10;
    public float Friction = 0.2f;
    public float MaxSpeed;
    public float JumpControl = 0.2f;
    public float ChangeDirectionControl = 2f;

    private Rigidbody _rigidbody;
    private bool isGrounded;
    private Vector3 _startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        ScalePlayer();
        Jump();
        Rotate();
    }

    private void ScalePlayer()
    {
        Vector3 targetScale;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) || !isGrounded)
        {
            targetScale = new Vector3(1f, 0.5f, 1f);
        }
        else
        {
            targetScale = Vector3.one;
        }

        ColliderTransform.localScale = Vector3.Lerp(ColliderTransform.localScale, targetScale, Time.deltaTime * 10f);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Подобрал эту функцию по замерам разных JumpSpeed
            // Осознаю, что скорее всего можно сделать умнее
            float jumpSpeed = Mathf.Sqrt(41 * JumpHeight); 
            _rigidbody.AddForce(0, jumpSpeed, 0f, ForceMode.VelocityChange);
        }
    }

    private void Rotate()
    {
        Vector3 mouseWorldPosition = MouseUtils.GetMouseIntersectionWithPlane();
        Vector3 aimDirection = mouseWorldPosition - Body.position;
      
        float dotProduct = ClampedDotProduct(Vector3.left, aimDirection);
        Quaternion targetRotation = Quaternion.Euler(0f, 45f * dotProduct, 0f);
        Body.rotation = Quaternion.Lerp(Body.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private float ClampedDotProduct(Vector3 lhs, Vector3 rhs)
    {
        float dotProduct = Vector3.Dot(lhs, rhs);
        if (dotProduct < 0f) return -1f;
        return 1f;
    }

    void FixedUpdate()
    {
        float speedMultiplier = 1;
        if (!isGrounded)
        {
            // Если знак скорости и инпута одинаковые, то игрок движется в том же направлении что и раньше
            int inputSign = (int) Mathf.Sign(Input.GetAxis("Horizontal"));
            int velocitySign = (int) Mathf.Sign(_rigidbody.velocity.x);
            bool isSameDirection = (inputSign == velocitySign);

            speedMultiplier = JumpControl;
            if (Mathf.Abs(_rigidbody.velocity.x) >= MaxSpeed)
            {
                Vector3 velocity = _rigidbody.velocity;
                _rigidbody.velocity = new Vector3(velocitySign * MaxSpeed, velocity.y, velocity.z);
                
                if (isSameDirection)
                {
                    speedMultiplier = 0;
                } 
            }

            // Условие для того, чтобы сделать персонажа более управляемым, во время смены движения в прыжке
            if (!isSameDirection && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                speedMultiplier = ChangeDirectionControl;
            }
        }

        _rigidbody.AddForce(Input.GetAxis("Horizontal") * MoveSpeed * speedMultiplier, 0f, 0f, ForceMode.VelocityChange);
        
        if (isGrounded)
        {
            _rigidbody.AddForce(-_rigidbody.velocity.x * Friction, 0f, 0f, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = _startPosition;
    }

    private void OnCollisionStay(Collision other)
    {
        float angle = Vector3.Angle(other.contacts[0].normal, Vector3.up);
        if (angle < 45)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
    
}
