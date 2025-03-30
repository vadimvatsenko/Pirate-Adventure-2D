using UnityEngine.SceneManagement;

public class ReloadLevelComponent
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
