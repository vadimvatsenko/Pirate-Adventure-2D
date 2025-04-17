using Components.EnterCollisionComponent;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиях
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag(gameObjectTag))
            {
                onAction.Invoke(other.gameObject);
            }
        }
    }
}