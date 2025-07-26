using Controllers;
using Creatures;
using Creatures.CreaturesStateMachine.Hero;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public class DeadZone : MonoBehaviour
    {
        // можно также создать событие и вызвать его в триггере
        // [SerializeField] private UnityEvent onDead;

        [FormerlySerializedAs("enterPoint")] [SerializeField] private GameManager gameManager;
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

