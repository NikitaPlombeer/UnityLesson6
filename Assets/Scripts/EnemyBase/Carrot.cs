using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Carrot : MonoBehaviour
{
    public float Speed = 3f;
    
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Transform player = FindObjectOfType<PlayerController>().transform;
        
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = toPlayer * Speed;
    }

}