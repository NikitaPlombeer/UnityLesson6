using UnityEngine;

public class Acorn : MonoBehaviour
{

    public Vector3 Velocity;
    public float MaxRotationSpeed;
    
    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Velocity, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().angularVelocity = GetRandomRotationSpeedVector();
    }

    private Vector3 GetRandomRotationSpeedVector()
    {
        return new Vector3(GetRandomRotationSpeed(), GetRandomRotationSpeed(), GetRandomRotationSpeed());
    }

    private float GetRandomRotationSpeed()
    {
        return Random.Range(-MaxRotationSpeed, MaxRotationSpeed);
    }
}
