using UnityEngine;

namespace EnemyBase
{
    public class MakeDamageOnCollision : MonoBehaviour
    {

        public int DamageValue = 1;
    
        private void OnCollisionEnter(Collision other)
        {
            Rigidbody otherRigidbody = other.rigidbody;
            if (!otherRigidbody) return;
    
            PlayerHealth playerHealth = otherRigidbody.GetComponent<PlayerHealth>();
            if (!playerHealth) return;

            playerHealth.TakeDamage(DamageValue);
        }
    }
}
