using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfLevel : MonoBehaviour
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
