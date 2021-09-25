using UnityEngine;

namespace EnemyBase
{
    public class TakeDamageOnTrigger : MonoBehaviour
    {
        public EnemyHealth Health;
        public int Damage = 1;
        public bool DieOnAnyCollision;

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody otherRigidbody = other.attachedRigidbody;
            if (otherRigidbody)
            {
                Bullet bullet = otherRigidbody.GetComponent<Bullet>();
                if (bullet)
                {
                    Health.TakeDamage(Damage); 
                    bullet.Die();
                }
            }
         

            if (DieOnAnyCollision)
            {
                if (other.isTrigger == false)
                {
                    Health.TakeDamage(1000);
                }
            }
        }
        
    }
}
