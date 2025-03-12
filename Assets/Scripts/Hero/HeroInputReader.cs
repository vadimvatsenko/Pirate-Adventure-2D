using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private Hero hero; // если мы захотим связать через редактор

    private HeroInputAction _heroInputAction;
    
    private void Awake()
    {
        _heroInputAction = new HeroInputAction(); // класс который мы создали
        
        // нужно подписыватся на начало ввода и завершение ввода
        // performed - начало ввода
        // canceled - конец ввода
        _heroInputAction.Hero.HorizontalMovement.performed += OnHorizontalMovement; 
        _heroInputAction.Hero.HorizontalMovement.canceled += OnHorizontalMovement;
        _heroInputAction.Hero.Fire.performed += OnShoot;
    }
    private void OnEnable()
    {
        _heroInputAction.Enable(); // тут активируем прослушку событий
    }
    private void OnDisable()
    {
        _heroInputAction.Hero.HorizontalMovement.performed -= OnHorizontalMovement;
        _heroInputAction.Hero.HorizontalMovement.canceled -= OnHorizontalMovement;
        _heroInputAction.Hero.Fire.performed -= OnShoot;
    }

    private void OnHorizontalMovement(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<float>();
        Debug.Log(direction);
        hero.SetDirection(direction);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if(context.canceled) hero.Fire();
    }
}
