using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterCollisionComponents
{
    public class EnterTriggerSimpleComponent :MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиях
        [SerializeField] private UnityEvent onAction; // класс который мы создали в серилизации
        
        [SerializeField] private EnterVector2Event onEnter;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(gameObjectTag))
            {
                onAction?.Invoke();
                onEnter?.Invoke(this.transform.position);
            }
        }
    }
}