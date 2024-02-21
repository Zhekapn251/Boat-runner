using System;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.BoatLogic
{
    [RequireComponent(typeof(WaterFloat))]
    public class BoatMover : MonoBehaviour
    {
        private float clampDeviation = 6f;
        [SerializeField] private float _steerPower = 100f;
        [SerializeField] private float _power = 5f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private Transform _finish;
        

        private Rigidbody _rigidbody;
        private bool _isSwiping;
        private float _direction;
        private bool _allowLateralMovement;
        private bool _allowForwardMovement = true;
        private LevelStateMachine _levelStateMachine;
        private PlayerStatsChangerService _playerStatsChangerService;
        private float _initialLevelLength;
        private IInputService _inputService;

        [Inject]
        public void Construct(LevelStateMachine levelStateMachine, PlayerStatsChangerService playerStatsChangerService, IInputService inputService)
        {
            _inputService = inputService;
            _playerStatsChangerService = playerStatsChangerService;
            _levelStateMachine = levelStateMachine;
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _initialLevelLength = Vector3.Distance(transform.position, _finish.position);
            _levelStateMachine.OnStateChanged += OnStateChanged;
        }
        
        private void Update()
        {
            _isSwiping = _inputService.IsSwiping;
            _direction = _inputService.Direction;
        }

        public void FixedUpdate()
        {
            if (_isSwiping && _allowLateralMovement) ApplyLateralForce();
            if (_allowForwardMovement)
            {
                ApplyForwardMovement();
                CalculateLeveLength();
            }
            ClampPositionByXAxis();
        }


        private void OnStateChanged(IState state)
        {
            switch (state)
            {
                case PlayingLevelState _:
                     _allowLateralMovement = true;
                     _allowForwardMovement = true;
                    break;
                case WinLevelState _:
                    _allowLateralMovement = false;
                    _allowForwardMovement = false;
                    break;
                case WinBossLevelState _:
                    _allowLateralMovement = true;
                     _allowForwardMovement = true;
                    break;
                case LoseLevelState _:
                    _allowLateralMovement = false;
                     _allowForwardMovement = false;
                    break;
                case BossFightLevelState _:
                    _allowLateralMovement = true;
                    _allowForwardMovement = false;
                    break;
            }
        }

        private void CalculateLeveLength()
        {
            var levelLength = Vector3.Distance(transform.position, _finish.position);
            var progress = 1-levelLength/_initialLevelLength;
            _playerStatsChangerService.ProgressLevelChanged(progress);
        }

        private void ClampPositionByXAxis()
        {
            Vector3 position = _rigidbody.position;

            position.x = Mathf.Clamp(position.x, -clampDeviation, clampDeviation);

            _rigidbody.position = position;
        }

        private void ApplyLateralForce()
        {
            Vector3 forceDirection = transform.right * _direction * _steerPower;
            _rigidbody.AddForce(forceDirection, ForceMode.VelocityChange);
        }


        private void ApplyForwardMovement()
        {
            if (!_allowForwardMovement) return; 
            if (_rigidbody.velocity.magnitude < _maxSpeed)
            {
                Vector3 forwardForce = transform.forward * _power;

                _rigidbody.AddForce(forwardForce, ForceMode.Acceleration);
            }
        }
        
    }
}