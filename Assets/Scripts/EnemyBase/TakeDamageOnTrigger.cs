using UnityEngine;

namespace EnemyBase
{
    public class TakeDamageOnTrigger : MonoBehaviour
    {
        public EnemyHealth Health;
        public bool DieOnAnyCollision;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody) return;
        
            if (other.attachedRigidbody.GetComponent<Bullet>())
            {
                Health.TakeDamage(1); 
            }

            if (DieOnAnyCollision)
            {
                Health.TakeDamage(1000);
            }
        }
        
    }
}
