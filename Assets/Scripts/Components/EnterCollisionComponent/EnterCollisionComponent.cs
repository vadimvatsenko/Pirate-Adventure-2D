using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterCollisionComponent
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации
        [SerializeField] private UnityEvent onExit;
        [SerializeField] private UnityEvent onEnter;

        [SerializeField] private bool isDot; // проверяем ли столкновение в определённой точке коллайдера?
        [SerializeField] private Directions dotDirection; // направление проверки
        [SerializeField] private float delayOnEnter = 0.1f;
        
        private bool _objectStillOnPlatform = false;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.gameObject.CompareTag(gameObjectTag) && !_objectStillOnPlatform)
            {
                
                if (isDot)
                {
                    switch (dotDirection)
                    {
                        case Directions.Right:
                            DotTest(collision, Vector2.right);
                            break;
                        case Directions.Left:
                            DotTest(collision, Vector2.left);
                            break;
                        case Directions.Bottom:
                            DotTest(collision, Vector2.down);
                            break;
                        case Directions.Top:
                            DotTest(collision, Vector2.up);
                            break;
                    }
                }
                else
                {
                    _objectStillOnPlatform = true;
                    onEnter?.Invoke();
                    onAction?.Invoke(collision.gameObject);
                }
            }
        }

        private IEnumerator HandleEnterDelayed()
        {
            yield return new WaitForSeconds(delayOnEnter);
            _objectStillOnPlatform = false;
            onEnter?.Invoke();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(gameObjectTag))
            {
                onExit?.Invoke();
                StartCoroutine(HandleEnterDelayed());
            }
        }
        

        private void DotTest(Collision2D collision, Vector2 direction)
        {
            foreach (var contact in collision.contacts)
            {
                if (Vector2.Dot(contact.normal, direction) > 0.5f)
                {
                    onAction?.Invoke(collision.gameObject);
                }
            }
        }
    }
}