using System.Collections;
using Cinemachine;
using PlayerFolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class CameraSwitcherComponent :MonoBehaviour
    {
        [SerializeField] private Transform playerTarget;
        [SerializeField] private Transform doorTarget;

        private Transform _tempTarget;
        
        private CinemachineVirtualCamera _vcam;
        private float _moveTime = 0.75f;
        private float _elapsedTime = 0f;

        private void Awake()
        {
            _tempTarget = new GameObject("TempFollow").transform;
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        public void FocusDoor()
        {
            StartCoroutine(MoveToTarget());
        }

        public IEnumerator MoveToTarget()
        {
            playerTarget.GetComponent<PlayerInput>().enabled = false;
            
            yield return StartCoroutine(LerpCamera(playerTarget, doorTarget));
            yield return new WaitForSeconds(_moveTime);
            yield return StartCoroutine(LerpCamera(doorTarget, playerTarget));
            
            playerTarget.GetComponent<PlayerInput>().enabled = true;
        }

        private IEnumerator LerpCamera(Transform from, Transform to)
        {
            _elapsedTime = 0f;
            while (_elapsedTime < _moveTime)
            {
                _elapsedTime += Time.deltaTime;
                
                float t = _elapsedTime / _moveTime;
                _tempTarget.position = Vector3.Lerp(from.position, to.position, t);
                
                _vcam.Follow = _tempTarget;
                yield return null;
            }
            _vcam.Follow = to;
        }
    }
}