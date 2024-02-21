using System;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Interfaces
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine); 
        void PerformAfterDelay(float delay, Action action);
    }
}