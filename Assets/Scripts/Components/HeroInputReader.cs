using PlayerFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class HeroInputReader : MonoBehaviour
    {
        //[SerializeField] private Hero hero; // если мы захотим связать через редактор
        
        // временно, потом удалю
        private Player _player; 
    
        // Название должно совпадать с тем, что настроено в системе с приставкой On

        private void Start()
        {
            _player = FindObjectOfType<Player>();
        }
        private void OnMovement(InputValue context) 
        {
            float direction = context.Get<float>();
            if (_player != null) _player.SetDirection(direction); 
        }

        private void OnJump(InputValue context)
        {
            // Передаем значение нажата ли кнопка, вызов происходит много раз. Так как значение у кнопки Value Axis
            _player.HandleJump(context.isPressed);
        }

        private void OnInteract(InputValue context)
        {
            _player.Interact();
        }

        private void OnHit(InputValue context) // временно
        {
            _player.TakeDamage();
        }
    }
}

