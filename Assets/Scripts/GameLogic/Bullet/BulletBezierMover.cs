using System;
using System.Collections;
using GameLogic.Interfaces;
using Infrastructure;
using Infrastructure.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GameLogic.Bullet
{
    public class BulletBezierMover : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;

        private Vector3 _target;
        private Vector3 _startPoint;
        private Vector3 _intermediatePoint;

        private bool _isMoving;
        private float _step = 0;
        private BulletAttack _bulletAttack;
        private BossBattleService _bossBattleService;
       
        public float radius = 5f; 
        public LayerMask layerMask = (1 << Constants.ENEMY_LAYER_NUMBER) | (1 << Constants.BOAT_LAYER_NUMBER); 

        


       
        public void SetBossBattleService(BossBattleService bossBattleService)
        {
            _bossBattleService = bossBattleService;
        }
        

        public void ActivateBullet(Vector3 startPoint, Vector3 target)
        {
            _startPoint = startPoint;
            _target = target;
            GenerateIntermediatePoint();
            _isMoving = true;
        }

        private void OnBulletHit()
        {
            Debug.Log("Bullet hit");
            if(_isMoving)
                _bossBattleService.EndTurn();
            _isMoving = false;
        }

        IEnumerator OnTargetReached()
        {
            yield return new WaitForSeconds(0.5f);
//            Debug.Log("Target reached");
            if(_isMoving)
                _bossBattleService.EndTurn();
            _isMoving = false;
            //spawn OverlapSphere
            //spawn explosionVFX
            //play sound
            
            SpawnCollidersDetectionSphere();

            StartCoroutine(EndTurn());
            Destroy(gameObject);
        }
            
        IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(3);
            _bossBattleService.EndTurn();
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            
            Vector3 nextPositionOnCurve =
                CalculateBezierPoint(_step, _startPoint, _intermediatePoint, _target);
          
            
            transform.position = Vector3.MoveTowards(transform.position, nextPositionOnCurve,
                speed * Time.deltaTime);
            transform.LookAt(_target);
           
            if (_step < 1.0f)
            {
                Vector3 currentPosition = transform.position;
                Vector3 direction = (nextPositionOnCurve - currentPosition).normalized;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction); 
                    transform.rotation = targetRotation;
                } 
 
                _step += Time.deltaTime * speed / Vector3.Distance(_startPoint, _target);
            }
            else
            {
                StartCoroutine(OnTargetReached());
            }
        }

        void SpawnCollidersDetectionSphere()
        {
            Collider[] results = new Collider[5]; 
            int num = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask);
            for (int i = 0; i < num; i++)
            {
                IDamageable damageable = results[i].GetComponent<IDamageable>();
                if (damageable != null)
                {
         //           Debug.Log("Collided with " + results[i].name);
//                    Debug.Log("Taking damage  = 10;");
                    damageable.TakeDamage(10);
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }
        private void GenerateIntermediatePoint()
        {
            Vector3 startPoint = _startPoint;
            Vector3 targetPoint = _target;
            Vector3 midPoint = (startPoint + targetPoint) / 2;

            float halfDistance = Vector3.Distance(startPoint, targetPoint) / 2;
            Vector3 deviationVector = new Vector3(
                Random.Range(-halfDistance, halfDistance),
                Random.Range(0, halfDistance),
                Random.Range(-halfDistance, halfDistance));

            _intermediatePoint = midPoint + deviationVector;
        }
    }
}