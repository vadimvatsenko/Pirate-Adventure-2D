using System;
using Controllers.Cheats;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    [SerializeField] private float currentLiveTime; // время жизни чита
    [SerializeField] private CheatItem[] cheatItems; // список читов, для этого мы серилиазовали класс CheatItem

    private string _currentInput; // в строку запись символов
    private float _inputTime;
    private void Awake()
    {
        // Keyboard вешаем на него событие, будем получать ввод с клавиатуры
        Keyboard.current.onTextInput += OnTextInput; 
    }
    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void Update()
    {
        if (_inputTime < 0) // если время истекло, обнуляем строку
        {
            _currentInput = String.Empty;
        }

        else
        {
            _inputTime -= Time.deltaTime; // в противном случае отсчитываем время
        }
    }
    private void OnTextInput(char inputChar)
    {
        _currentInput += inputChar; // += записует символы в строку, добавляя символ уже к существующему
        _inputTime = currentLiveTime; // пока вводим чит, пока и будет обновляться время на начальное значение

        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheat in cheatItems)
        {
            if (_currentInput.Contains(cheat.Name)) // проходимся по читам, если нашли, то вызвали событие на нем
            {
                cheat.Action?.Invoke();
                _currentInput = String.Empty;
            }
        }
    }
}
