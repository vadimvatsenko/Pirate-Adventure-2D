using UnityEngine;
using UnityEngine.Events;

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
            enterPoint.ReloadLevelComponent.ReloadLevel();
            
            // onDead?.Invoke(); вызов события
        }
    }
}
