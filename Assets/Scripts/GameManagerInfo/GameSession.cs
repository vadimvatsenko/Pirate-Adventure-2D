using System.Collections;
using UnityEngine;

namespace GameManagerInfo
{
    // Этот класс GameSession реализует паттерн Singleton per scene для хранения данных о сессии игрока
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        public PlayerData PlayerData => playerData;
        
        //Temp
        private LevelController _levelController;
        public LevelController LevelController => _levelController;
        
        private void Awake()
        {
            // Если уже существует другой объект GameSession, текущий уничтожается.
            if (IsSessionExit())
            {
                DestroyImmediate(this);
            }
            // Если нет — сохраняется между сценами с помощью DontDestroyOnLoad(this).
            else
            {
                DontDestroyOnLoad(this);
            }
            
            _levelController = new LevelController();
        }

        // Находит все объекты GameSession в сцене:
        // Если находится другой (не текущий) экземпляр — возвращает true (такая сессия уже есть).
        // Иначе — false.
        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();

            foreach (var gameSession in sessions)
            {
                if(gameSession != this) return true;
            }
            return false;
        }
        
        // Temp
        public void LoadNextLevel()
        {
            _levelController.LoadNextLevel();
        }

        public void ReloadLevel() => StartCoroutine(ReloadLevelWhithDelay(2f));
        
        private IEnumerator ReloadLevelWhithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _levelController.ReloadLevel();
        }
    }
}