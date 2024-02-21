using System;
using System.Collections;
using Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader
    {
        private readonly ICoroutineHandler _coroutineHandler;
        
        public SceneLoader(ICoroutineHandler coroutineHandler) => 
            _coroutineHandler = coroutineHandler;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineHandler.StartCoroutine(LoadScene(name, onLoaded));
    
        public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }
    }
}