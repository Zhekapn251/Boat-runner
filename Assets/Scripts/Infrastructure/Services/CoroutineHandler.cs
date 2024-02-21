using System;
using System.Collections;
using Infrastructure.Interfaces;
using UnityEngine;

namespace Infrastructure.Services
{
    public class CoroutineHandler : MonoBehaviour, ICoroutineHandler
    {
        public void PerformAfterDelay(float delay, Action action)
        {
            StartCoroutine(PerformActionAfterDelayCoroutine(delay, action));
        }
        
        private IEnumerator PerformActionAfterDelayCoroutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}
