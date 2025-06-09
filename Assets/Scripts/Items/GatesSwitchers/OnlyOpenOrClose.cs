using System;
using UnityEngine.Events;

namespace Items
{
    [Serializable]
    public class OnlyOpenOrClose : UnityEvent<GateSwithDirection>
    {
        
    }
}