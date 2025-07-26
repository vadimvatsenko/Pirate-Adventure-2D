using UnityEngine.SceneManagement;

namespace GameManagerInfo
{
    public class LevelController
    {
        public void ReloadLevel() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        public void LoadNextLevel() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

