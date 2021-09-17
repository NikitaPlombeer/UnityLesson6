using UnityEngine;

namespace EnemyBase
{
    public class MakeDamageOnTrigger : MonoBehaviour
    {

        public int DamageValue = 1;

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody otherRigidbody = other.attachedRigidbody;
            if (!otherRigidbody) return;
    
            PlayerHealth playerHealth = otherRigidbody.GetComponent<PlayerHealth>();
            if (!playerHealth) return;

            playerHealth.TakeDamage(DamageValue);
        }

    }
}
