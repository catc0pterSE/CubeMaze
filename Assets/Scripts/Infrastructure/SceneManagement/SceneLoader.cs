using System;
using System.Collections;
using Infrastructure.CoroutineRunner;
using UI.Loading;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(LoadingCurtain loadingCurtain, ICoroutineRunner coroutineRunner)
        {
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            _loadingCurtain.FadeIn( () => _coroutineRunner.StartCoroutine(Load(name, onLoaded)));
        }

        private IEnumerator Load(string sceneName, Action onLoaded)
        {
            var load = SceneManager.LoadSceneAsync(sceneName);

            while (load.isDone == false)
                yield return null;
            
            onLoaded.Invoke();
            _loadingCurtain.FadeOut();
        }
    }
}