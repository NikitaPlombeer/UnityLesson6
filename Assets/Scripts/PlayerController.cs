using System;
using System.Collections;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    
    public Transform Body;
    public Transform ColliderTransform;
    
    public float MoveSpeed = 5;
    public float Friction = 0.2f;
    public float MaxSpeed = 7;
   
    private bool isGrounded;
    public float JumpControl = 0.2f;
    public float JumpMaxHeight = 3f;
    public float JumpMaxLength = 6f;
    private float _jumpGravity = 9.87f;
    private bool _isJumping; 
    private float _timeFromJump;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        CalculateGravity();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + JumpMaxLength * Vector3.right;
        float count = 20;
        Vector3 lastP = start;
        for (float i = 0; i < count + 1; i++) {
            Vector3 p = GizmosUtils.SampleParabola (start, end, JumpMaxHeight, i / count);
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine (lastP, p);
            lastP = p;
        }
        
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
        if (!_isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
         
            float xh = JumpMaxLength / 2f;
             float v0 = 2 * JumpMaxHeight * MaxSpeed / xh; // aka Up speed
            float velocityX = _rigidbody.velocity.x;
            if (velocityX < 0.01f) velocityX = 0f;
            _rigidbody.velocity = new Vector3(velocityX, v0, 0f);

            StartCoroutine(GroundCheckAfterJump());
        }
    }

    private void CalculateGravity()
    {
        float xh = JumpMaxLength / 2f;
        _jumpGravity = -2 * JumpMaxHeight * Mathf.Pow(MaxSpeed, 2) / Mathf.Pow(xh, 2);
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
        if (_isJumping && isGrounded && _timeFromJump > 0.2f)
        {
            _isJumping = false;
        }

        MovePlayer();
        ApplyGravity();
    }

    private IEnumerator GroundCheckAfterJump()
    {
        _timeFromJump = 0f;
        while (_isJumping)
        {
            _timeFromJump += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void MovePlayer()
    {
        float speedMultiplier = 1;
        if (!isGrounded)
        {
            speedMultiplier = GetSpeedMultiplierInAir();
            AdjustVelocityIfRequired();
        }

        float xForce = Input.GetAxis("Horizontal") * MoveSpeed * speedMultiplier;
        _rigidbody.AddForce(xForce, 0f, 0f, ForceMode.VelocityChange);
        
        if (!_isJumping && isGrounded)
        {
            _rigidbody.AddForce(-_rigidbody.velocity.x * Friction, 0f, 0f, ForceMode.VelocityChange);
        }
    }

    private float GetSpeedMultiplierInAir()
    {
        float speedMultiplier = JumpControl;
        if (!IsMaxSpeedReached()) return speedMultiplier;
        
        // Если знак скорости и инпута одинаковые, то игрок движется в том же направлении что и раньше
        bool isSameDirection = IsSignTheSame(
            Input.GetAxis("Horizontal"), 
            _rigidbody.velocity.x
        );
        if (isSameDirection)
        {
            speedMultiplier = 0;
        }

        return speedMultiplier;
    }
    
    private void AdjustVelocityIfRequired()
    {
        if (!IsMaxSpeedReached()) return;

        int velocitySign = (int) Mathf.Sign(_rigidbody.velocity.x);
        Vector3 currentVel = _rigidbody.velocity;
        _rigidbody.velocity = new Vector3(velocitySign * MaxSpeed, currentVel.y, currentVel.z);
    }

    private bool IsMaxSpeedReached()
    {
        return Mathf.Abs(_rigidbody.velocity.x) > MaxSpeed;
    }

    private bool IsSignTheSame(float v1, float v2)
    {
        int s1 = (int) Mathf.Sign(v1);
        int s2 = (int) Mathf.Sign(v2);
        return (s1 == s2);
    }
    
    private void ApplyGravity()
    {
        var currentVel = _rigidbody.velocity;
        currentVel.y += _jumpGravity * Time.deltaTime;
        _rigidbody.velocity = currentVel;
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
