using UnityEngine.SceneManagement;

namespace Controllers
{
    public class ReloadLevelController
    {
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

