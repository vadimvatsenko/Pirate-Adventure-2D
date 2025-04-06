using UnityEngine;

namespace Components.EnterCollisionComponent
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string gameobjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(gameobjectTag))
            {
                Debug.Log(gameobjectTag + " entered");
                onAction?.Invoke(collision.gameObject); // тут будем вызывать событие
            }
        }
    }
}