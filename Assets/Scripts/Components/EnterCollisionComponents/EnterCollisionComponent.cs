using UnityEngine;
using UnityEngine.Events;

// Важный, передает в событие Обьект
namespace Components.EnterCollisionComponents
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации
        
        [SerializeField] private UnityEvent onEnter;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (gameObjectTag.Equals(collision.gameObject.tag))
            {
                onAction?.Invoke(collision.gameObject);
                onEnter.Invoke();
            }
        }
    }
}