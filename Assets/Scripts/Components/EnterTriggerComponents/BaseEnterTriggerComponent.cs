using Animation.EditorHelpers;
using Components.EnterCollisionComponents;
using Creatures.CreaturesStateMachine.Player;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterTriggerComponents
{
    public class BaseEnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации
        [SerializeField] private UnityEvent onEnter;
        
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