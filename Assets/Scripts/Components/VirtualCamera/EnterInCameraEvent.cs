using System;
using Cinemachine;
using UnityEngine.Events;

namespace Components.VirtualCamera
{
    [Serializable]
    public class EnterInCameraEvent : UnityEvent<CinemachineVirtualCamera>
    {
        
    }
}