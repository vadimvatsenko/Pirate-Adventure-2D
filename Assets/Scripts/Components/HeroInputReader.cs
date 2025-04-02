using PlayerFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class HeroInputReader : MonoBehaviour
    {
        //[SerializeField] private Hero hero; // если мы захотим связать через редактор
        
        // временно, потом удалю
        private Player player; 
    
        // Название должно совпадать с тем, что настроено в системе с приставкой On

        private void Start()
        {
            player = FindObjectOfType<Player>();
        }
        private void OnMovement(InputValue context) 
        {
            float direction = context.Get<float>();
            if (player != null) player.SetDirection(direction); 

        }

        private void OnJump(InputValue context)
        {
            // Передаем значение нажата ли кнопка, вызов происходит много раз. Так как значение у кнопки Value Axis
            player.HandleJump(context.isPressed); 
        }
    }
}

