using UnityEngine;
using UnityEngine.Events;

namespace EnemyBase
{
    public class EnemyHealth : MonoBehaviour
    {

        public int Health = 1;
        public UnityEvent OnTakeDamage;
        
        public void TakeDamage(int damageValue)
        {
            Health -= damageValue;
            if (Health <= 0)
            {
                Die();
            }
            OnTakeDamage.Invoke();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
