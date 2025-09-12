using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.EnterCollisionComponents
{
    // делает класс сериализуемым, чтобы его можно было редактировать через Inspector в Unity.
    [Serializable]
    
    // этот класс наследуется от UnityEvent<GameObject>,
    // т.е. это событие, которое может быть вызвано с параметром типа GameObject.
    public class EnterEvent : UnityEvent<GameObject>
    {
        
    }
}