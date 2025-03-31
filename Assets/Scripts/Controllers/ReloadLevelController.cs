using UnityEngine.SceneManagement;

public class ReloadLevelController
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
