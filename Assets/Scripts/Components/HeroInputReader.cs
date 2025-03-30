using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero hero; // если мы захотим связать через редактор
    
    // Название должно совпадать с тем, что настроено в системе с приставкой On
    private void OnMovement(InputValue context) 
    {
        float direction = context.Get<float>();
        hero.SetDirection(direction);
    }

    private void OnJump(InputValue context)
    {
        // Передаем значение нажата ли кнопка, вызов происходит много раз. Так как значение у кнопки Value Axis
        hero.HandleJump(context.isPressed); 
    }
}
