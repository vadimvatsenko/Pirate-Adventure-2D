using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class ExitLevelComponent :MonoBehaviour
    {
        [SerializeField] private string sceneName;
        public void Exit()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}