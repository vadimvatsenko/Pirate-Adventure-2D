using Creatures.CreaturesStateMachine.Player;
using GameManagerInfo;
using UnityEngine;

namespace Components
{
    public class DeadZone : MonoBehaviour
    {
        // можно также создать событие и вызвать его в триггере
        // [SerializeField] private UnityEvent onDead;

        [SerializeField] private GameManager gameManager;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            
            if (hero)
            {
                gameManager.LevelController.ReloadLevel();
                // onDead?.Invoke(); вызов события
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

