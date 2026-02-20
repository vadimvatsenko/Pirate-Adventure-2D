using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterCollisionComponents
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации
        
        [SerializeField] private UnityEvent onEnter;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObjectTag.Equals(other.gameObject.tag))
            {
                onAction?.Invoke(other.gameObject);
                onEnter.Invoke();
            }
        }
    }
}