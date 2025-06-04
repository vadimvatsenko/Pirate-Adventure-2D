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
            Hero hero = other.GetComponent<Hero>();
            if (hero != null)
            {
                onEnterInCameraZone?.Invoke(currentCamera);
            }
        }
    }
}