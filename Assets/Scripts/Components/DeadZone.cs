using Controllers;
using Creatures;
using Creatures.CreaturesStateMachine.Hero;
using UnityEngine;

namespace Components
{
    public class DeadZone : MonoBehaviour
    {
        // можно также создать событие и вызвать его в триггере
        // [SerializeField] private UnityEvent onDead;

        [SerializeField] private EnterPoint enterPoint;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            
            if (hero)
            {
                enterPoint.ReloadLevelController.ReloadLevel();
                // onDead?.Invoke(); вызов события
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

