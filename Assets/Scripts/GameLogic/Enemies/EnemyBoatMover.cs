using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.Enemies
{
    public class EnemyBoatMover: MonoBehaviour
    {
        [SerializeField] private float clampDeviation = 8f;
        [SerializeField] private float _power = 5f;
        [SerializeField] private float _maxSpeed = 5f;

        private Rigidbody _rigidbody;
        private float _direction;


        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }



        public void FixedUpdate()
        {
            ApplyForwardMovement();
           ClampPositionByXAxis();
        }

        

        private void ClampPositionByXAxis()
        {
            Vector3 position = _rigidbody.position;

            position.x = Mathf.Clamp(position.x, -clampDeviation, clampDeviation);

            _rigidbody.position = position;
        }
        
        private void ApplyForwardMovement()
        {
            if (_rigidbody.velocity.magnitude < _maxSpeed)
            {
                Vector3 forwardForce = transform.forward * _power;

                _rigidbody.AddForce(forwardForce, ForceMode.Acceleration);
            }
        }
    }
}