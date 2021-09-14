using System;
using UnityEngine;

public class LootHeal : MonoBehaviour
{
    public int HealthValue = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.attachedRigidbody.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            Destroy(gameObject);
            playerHealth.AddHealth(HealthValue);
        }
    }
}
