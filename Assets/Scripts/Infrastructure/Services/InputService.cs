using Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class InputService : IInputService, ITickable
    {
        public bool IsSwiping { get; private set; }
        public int Direction { get; private set; }
        private Vector2 _startTouchPosition;
        private bool _isSwiping;


        private void UpdateInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwiping = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isSwiping)
                        {
                            Vector2 currentTouchPosition = touch.position;
                            Direction = (currentTouchPosition.x > _startTouchPosition.x) ? 1 : -1;
                            _startTouchPosition = currentTouchPosition;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isSwiping = false;
                        break;
                }
            }
            IsSwiping = _isSwiping;
        }

        public void Tick()
        {
            UpdateInput();
        }
    }
}