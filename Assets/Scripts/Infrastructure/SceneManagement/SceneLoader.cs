using System;
using UI.Loading;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly LoadingCurtain _loadingCurtain;

        public SceneLoader(LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
        }
        
        public void LoadScene(string name, Action onLoaded = null)
        {
           
        }
    }
}