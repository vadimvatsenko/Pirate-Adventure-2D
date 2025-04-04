using System;
using UnityEngine.Events;

namespace Controllers.Cheats
{
    [Serializable] // можно использовать для списка
    public class CheatItem
    {
        public string Name;
        public UnityEvent Action;
    }
}