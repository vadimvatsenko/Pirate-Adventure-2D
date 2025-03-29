using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero hero; // если мы захотим связать через редактор
    
    // Название должно совпадать с тем, что настроено в системе с приставкой On
    private void OnMovement(InputValue context) 
    {
        Vector2 direction = context.Get<Vector2>();
        hero.SetDirection(direction);
    }

    private void OnJump(InputValue context)
    {
        hero.Jump();
    }
}
