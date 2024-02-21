using System;
using System.Collections;
using DG.Tweening;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BossMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float rotationSpeed = 25f;
        [SerializeField] private Transform targetPointBeforeRandomMovement;
        private float _timeToTarget;

        private Vector2 boundaryX = new Vector2(-60f, 60f);
        private Vector2 boundaryZ = new Vector2(580f, 730);

        //private Rigidbody _rb;
        private Vector3 _targetDirection;
        private Quaternion _lookRotation;
        private float _timeToChangeDirection;
        private bool _canMove = true;
        private Vector3 _startPosition;
        private Vector3 _targetRandomPosition;
        private LevelStateMachine _levelStateMachine;
        private bool _firstEntryToLevelState = true;
        private BossBattleService _bossBattleService;
        private ICoroutineHandler _coroutineHandler;

        [Inject]
        public void Construct(LevelStateMachine levelStateMachine, BossBattleService bossBattleService, ICoroutineHandler coroutineHandler)
        {
            _coroutineHandler = coroutineHandler;
            _bossBattleService = bossBattleService;
            _levelStateMachine = levelStateMachine;
        }

        private void Start()
        {
            _startPosition = transform.position;
            DOTween.Init();
        }

        private void Update()
        {
            if (_levelStateMachine.CurrentState?.GetType() == typeof(BossFightLevelState) && _firstEntryToLevelState)
            {
                _firstEntryToLevelState = false;
                MoveToShowUpPosition();
            }
        }


        private void MoveToShowUpPosition()
        {
            var position = targetPointBeforeRandomMovement.position;
            _targetDirection = (position - _startPosition).normalized;
            transform.DOMove(position, speed).SetSpeedBased(true).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    StartMoving();
                    _bossBattleService.StartBattle();
                });
        }

        private void StartMoving()
        {
            _canMove = true;
            HandleMovement();
        }

        private void HandleMovement()
        {
            SetRandomTargetPosition();
            ChangeDirection();
            if (!_canMove || _targetDirection == Vector3.zero) return;

            _lookRotation = Quaternion.LookRotation(_targetDirection);
            transform.DORotateQuaternion(_lookRotation, rotationSpeed).SetSpeedBased(true).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    MoveToPosition(_targetRandomPosition);
                });
        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, speed).SetSpeedBased(true).SetEase(Ease.Linear)
                .OnComplete(HandleMovement);
        }
        
        private void ChangeDirection()
        {
            _targetDirection = (_targetRandomPosition - transform.position).normalized;
        }

        private void SetRandomTargetPosition()
        {
            float randomX = Random.Range(boundaryX.x, boundaryX.y);
            float randomZ = Random.Range(boundaryZ.x, boundaryZ.y);
            _targetRandomPosition = new Vector3(randomX, transform.position.y, randomZ);
        }
    }
}