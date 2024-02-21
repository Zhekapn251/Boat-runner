using GameLogic.Interfaces;
using UnityEngine;

namespace GameLogic.Enemies
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private Health health;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                health.Die();
            }
        }
    }
}
