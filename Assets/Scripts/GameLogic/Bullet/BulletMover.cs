using UnityEngine;
using Zenject;

namespace GameLogic.Bullet
{
    public class BulletMover: MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        public bool _isMoving;
        private BulletPool _bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool) =>
            _bulletPool = bulletPool;
        private void OnEnable()
        {
            Invoke(nameof(Disable), _lifeTime);
            _isMoving = true;
        }

        private void Update()
        {
            if (_isMoving)
                transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        private void Disable()
        {
            
            _isMoving = false;
            _bulletPool.ReturnBullet(GetComponent<Bullet>());
            gameObject.SetActive(false);
        }


    }
}