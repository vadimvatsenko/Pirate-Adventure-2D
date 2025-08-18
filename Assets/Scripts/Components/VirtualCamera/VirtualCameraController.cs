using System;
using System.Collections;
using Cinemachine;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Components.VirtualCamera
{
    public class VirtualCameraController : MonoBehaviour
    {
        private Hero _hero;
        
        [SerializeField] private CinemachineVirtualCamera startCamera;
        [SerializeField] private float delayTempCamera = 1.5f;
        
        private CinemachineVirtualCamera _preVirtualCamera;
        private CinemachineVirtualCamera _currentVirtualCamera;

        private void Awake()
        {
            if (startCamera != null)
            {
                startCamera.Priority = 20;
            }
            
            _hero = FindObjectOfType<Hero>();
            _preVirtualCamera = startCamera;
            SetCameraAfterDead();
        }

        private void OnEnable()
        {
            _hero.SubscribeOnDeathEvent(SetCameraAfterDead);
        }

        private void OnDisable()
        {
            _hero.UnsubscribeOnDeathEvent(SetCameraAfterDead);
        }

        public void InteractiveObjectCameraActivated(CinemachineVirtualCamera camera) =>
            StartCoroutine(SwitchTempCameraRoutine(camera));

        private IEnumerator SwitchTempCameraRoutine(CinemachineVirtualCamera camera)
        {
            var originalCamera = _currentVirtualCamera;
            ActivateCamera(camera);
            yield return new WaitForSeconds(delayTempCamera);
            ActivateCamera(originalCamera);
        }

        public void ActivateCamera(CinemachineVirtualCamera targetCamera)
        {
            
            if (targetCamera == null || _currentVirtualCamera == targetCamera) return;

            _preVirtualCamera = _currentVirtualCamera;
            _currentVirtualCamera = targetCamera;
            SwapPriority();
        }
        
        private void SwapPriority()
        {
            if (_currentVirtualCamera != null)
                _currentVirtualCamera.Priority = 20;
    
            if (_preVirtualCamera != null)
                _preVirtualCamera.Priority = 10;
        }

        private void SetCameraAfterDead()
        {
            _currentVirtualCamera = startCamera;
            SwapPriority();
        }
    }
}