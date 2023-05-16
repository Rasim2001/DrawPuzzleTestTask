using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Infastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutine;

        public SceneLoader(ICoroutineRunner coroutine) =>
            _coroutine = coroutine;

        public void Load(string name, Action onLoaded = null)
        {
            _coroutine.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}