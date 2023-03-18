using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loader
{
    public class AppLoader : MonoBehaviour
    {
        protected void Start()
        {
            LoadGame();
        }
        
        private void LoadGame()
        {
            SceneManager.LoadScene(SceneNames.MainMenu);
        }
    }
}