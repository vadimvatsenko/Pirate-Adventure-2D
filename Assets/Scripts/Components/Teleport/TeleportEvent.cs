using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Teleport
{
    [Serializable]
    public class TeleportEvent : UnityEvent<Vector3>
    {
        
    }
}