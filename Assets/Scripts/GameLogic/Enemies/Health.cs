using GameLogic.Interfaces;
using Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace GameLogic.Enemies
{
    public class Health: MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] private ParticleSystem deathEffectPrefab;
        private AudioService _audioService;

        [Inject]
        public void Construct(AudioService audioService)
        {
            _audioService = audioService;
        }
        public void TakeDamage(float damage)
        {
            //Debug.Log("TakeDamage" + damage);
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
        public void Die()
        {
            _audioService.PlayEnemyDeath();
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}