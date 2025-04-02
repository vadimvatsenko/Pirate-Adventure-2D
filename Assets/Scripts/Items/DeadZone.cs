using Controllers;
using PlayerFolder;
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
            Player player = collision.GetComponent<Player>();
            Barrel barrel = collision.GetComponent<Barrel>();

            if (player)
            {
                enterPoint.ReloadLevelController.ReloadLevel();
                // onDead?.Invoke(); вызов события
            }

            if (barrel)
            {
                barrel.GetComponent<Barrel>().Destroy();
            }
        }
    }
}

