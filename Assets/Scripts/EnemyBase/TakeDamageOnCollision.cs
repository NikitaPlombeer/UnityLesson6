using UnityEngine;

namespace EnemyBase
{
    public class TakeDamageOnCollision : MonoBehaviour
    {
        public EnemyHealth Health;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.rigidbody) return;
        
            if (collision.rigidbody.GetComponent<Bullet>())
            {
                Health.TakeDamage(1); 
            }
        }
    }
}
