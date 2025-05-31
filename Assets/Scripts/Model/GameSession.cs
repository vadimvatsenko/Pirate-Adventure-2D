using UnityEngine;

namespace DefaultNamespace.Model
{
    // Этот класс GameSession реализует паттерн Singleton per scene для хранения данных о сессии игрока
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        
        public PlayerData PlayerData => playerData;

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
    }
}