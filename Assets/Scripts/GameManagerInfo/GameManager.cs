using System.Collections;
using TMPro;
using UnityEngine;

namespace GameManagerInfo
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI deathText;
        
        private LevelController _levelController;
        public LevelController LevelController => _levelController;
        private void Awake()
        {
            _levelController = new LevelController();
        }

        public void ChangeCoinsText(int score)
        {
            Debug.Log(score);
        }

        public void LoadNextLevel()
        {
            _levelController.LoadNextLevel();
        }

        public void ReloadLevel() => StartCoroutine(ReloadLevelWhithDelay(1f));
        
        private IEnumerator ReloadLevelWhithDelay(float delay)
        {
            deathText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            _levelController.ReloadLevel();
        }
    }
}

