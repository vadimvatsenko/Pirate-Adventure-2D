using UnityEngine;

namespace Components.EnterCollisionComponents
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиях
        [SerializeField] private bool destroyThisGameObjectAfterTrigger;
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(gameObjectTag))
            {
                onAction?.Invoke(other.gameObject);

                if (destroyThisGameObjectAfterTrigger)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}