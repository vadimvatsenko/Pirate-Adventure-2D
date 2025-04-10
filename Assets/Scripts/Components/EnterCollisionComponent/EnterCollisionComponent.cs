using UnityEngine;

namespace Components.EnterCollisionComponent
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string gameObjectTag; // тег с которым будем взаимодействиять
        [SerializeField] private EnterEvent onAction; // класс который мы создали в серилизации

        [SerializeField] private bool isDot; // проверяем ли столкновение в определённой точке коллайдера?
        [SerializeField] private Directions dotDirection; // направление проверки

        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(gameObjectTag))
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
                    
                    onAction?.Invoke(collision.gameObject);
                }
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