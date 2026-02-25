using Components.EnterCollisionComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterTriggerComponents
{
    public class BaseEnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] protected string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] protected EnterEvent onAction; // класс который мы создали в серилизации
        [SerializeField] protected UnityEvent onEnter;
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObjectTag.Equals(other.gameObject.tag))
            {
                onAction?.Invoke(other.gameObject);
                onEnter?.Invoke();
            }
        }
    }
}