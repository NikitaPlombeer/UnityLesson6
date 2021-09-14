using UnityEngine;

namespace EnemyBase
{
    public class EnemyHealth : MonoBehaviour
    {

        public int Health;

        public void TakeDamage(int damageValue)
        {
            Health -= damageValue;
            if (Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
