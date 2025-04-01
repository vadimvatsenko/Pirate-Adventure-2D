using Controllers;
using Player;
using UnityEngine;

namespace Items
{
    public class DeadZone : MonoBehaviour
    {
        // можно также создать событие и вызвать его в триггере
        // [SerializeField] private UnityEvent onDead;

        [SerializeField] private EnterPoint enterPoint;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            Barrel barrel = collision.GetComponent<Barrel>();

            if (hero || barrel)
            {
                enterPoint.ReloadLevelController.ReloadLevel();
            
                // onDead?.Invoke(); вызов события
            }
        }
    }
}

