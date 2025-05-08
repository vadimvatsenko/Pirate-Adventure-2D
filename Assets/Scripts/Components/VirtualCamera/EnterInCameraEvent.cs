using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [Serializable]
    public class EnterInCameraEvent : UnityEvent<CinemachineVirtualCamera>
    {
        
    }
}