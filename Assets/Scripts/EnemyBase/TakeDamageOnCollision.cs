using UnityEngine;

namespace EnemyBase
{
    public class TakeDamageOnCollision : MonoBehaviour
    {
        public EnemyHealth Health;
        public bool DieOnAnyCollision;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.rigidbody) return;
        
            if (collision.rigidbody.GetComponent<Bullet>())
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
