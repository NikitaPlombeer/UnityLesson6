using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hen : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _player;
    public float MaxSpeed = 3f;
    public float TimeToMaxSpeed = 1f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        Vector3 toPlayer = (_player.transform.position - transform.position).normalized;
        Vector3 force = _rb.mass * (toPlayer * MaxSpeed - _rb.velocity) / TimeToMaxSpeed;
        _rb.AddForce(force);
    }
}
