using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Components.VirtualCamera
{
    public class VirtualCameraController : MonoBehaviour
    {
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
    }
}