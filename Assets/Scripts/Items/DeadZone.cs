using Creatures.CreaturesStateMachine.Player;
using GameManagerInfo;
using UnityEngine;

namespace Items
{
    public class DeadZone : MonoBehaviour
    {
        // можно также создать событие и вызвать его в триггере
        // [SerializeField] private UnityEvent onDead;

        private GameSession _gameSession;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            
            if (hero)
            {
                _gameSession.LevelController.ReloadLevel();
                // onDead?.Invoke(); вызов события
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

