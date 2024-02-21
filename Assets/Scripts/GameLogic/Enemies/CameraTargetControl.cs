using System;
using DG.Tweening;
using GameLogic.BoatLogic;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GameLogic.Enemies
{
    public class CameraTargetControl : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler
    {
        [SerializeField] private Camera targetingCamera; 
        [SerializeField] private float sensitivity = 2f; 
        [SerializeField] private RectTransform dragArea;
        [SerializeField] private LayerMask bossLayer;
        [SerializeField] private CanvasGroup targetingCanvasGroup;
        private float _unfadeAnimationDuration = 2f;
        private float _fadeAnimationDuration = 0.5f;
        [SerializeField] private Boat boat;

        private Vector2 _initialTouchPosition;
        private Vector3 _cameraStartPosition;
        private BoatAttack _boatAttack;
        private BossBattleService _bossBattleService;
        private bool _isShooting;

        [Inject] 
        public void Construct(BossBattleService bossBattleService)
        {
            _bossBattleService = bossBattleService;
        }

        private void Start()
        {
            targetingCanvasGroup.interactable = false;
            targetingCanvasGroup.alpha = 0;
            _boatAttack = boat.GetComponent<BoatAttack>();
            _bossBattleService.OnBoatTurnStart += EnableTargeting;
        }

        private void OnDestroy()
        {
            _bossBattleService.OnBoatTurnStart -= EnableTargeting;
        }

        private void EnableTargeting() 
        { 
            targetingCanvasGroup.interactable = true;
            targetingCanvasGroup.DOFade(1, _unfadeAnimationDuration).From(0).OnComplete(
                () => _isShooting = true);
        }

        public void OnBeginDrag(PointerEventData eventData)
        { 
            if(!_isShooting)
                return;
            if (RectTransformUtility.RectangleContainsScreenPoint(dragArea, eventData.position, eventData.pressEventCamera))
            {
                _initialTouchPosition = eventData.position;
                _cameraStartPosition = targetingCamera.transform.localPosition;
            }
        }


        public void OnDrag(PointerEventData eventData)  
        {
            if(!_isShooting)
                return;
            if (RectTransformUtility.RectangleContainsScreenPoint(dragArea, eventData.position, eventData.pressEventCamera))
            {
                Vector3 worldPointDelta = targetingCamera.ScreenToViewportPoint(eventData.position) 
                                          - targetingCamera.ScreenToViewportPoint(_initialTouchPosition);
                
                Vector3 newPosition = _cameraStartPosition - worldPointDelta * sensitivity;
                targetingCamera.transform.localPosition = newPosition;
            }
        }

        private void FireMissile()
        { 
            Debug.Log("FireMissile");

            Ray ray = targetingCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, bossLayer))
            {
                Debug.Log("Hit");            
                DisableTargeting();
                _isShooting = false;
                Vector3 targetPoint = hit.point;
                _boatAttack.AttackBoss(targetPoint);
            }
        }

        private void DisableTargeting()
        {
            targetingCanvasGroup.interactable = false;
            
            targetingCanvasGroup.DOFade(0, _fadeAnimationDuration).From(1).OnComplete(
                () =>
                {
                    _isShooting = false;
                });
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!_isShooting)
                return;
            FireMissile();
        }


    }
}