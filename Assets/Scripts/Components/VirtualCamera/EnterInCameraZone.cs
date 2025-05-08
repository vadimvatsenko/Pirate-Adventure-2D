using System;
using Cinemachine;
using Components.EnterCollisionComponent;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class EnterInCameraZone : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        public EnterInCameraEvent onEnterInCameraZone;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                onEnterInCameraZone?.Invoke(currentCamera);
            }
        }
    }
}