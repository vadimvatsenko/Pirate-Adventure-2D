using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Cam
{
    public class CameraBoundsSwitcher: MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private float moveTime = 0.125f;
        
        private Coroutine _moveCoroutine;
        private Transform _followTarget;
        
        void Awake()
        {
            if (vcam == null) vcam = FindObjectOfType<CinemachineVirtualCamera>();

            // один общий таргет для Follow
            _followTarget = new GameObject("CameraFollowTarget").transform;
            _followTarget.position = vcam.transform.position; // стартовая
            vcam.Follow = _followTarget;
        }

        public void MoveTo(Vector2 worldPos)
        {
            var target = new Vector3(worldPos.x, worldPos.y, _followTarget.position.z);

            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine); // прервать текущий
            _moveCoroutine = StartCoroutine(MoveCoroutine(target));
        }

        private IEnumerator MoveCoroutine(Vector3 target)
        {
            Vector3 start = _followTarget.position;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / moveTime;
                float k = Mathf.SmoothStep(0f, 1f, t);
                _followTarget.position = Vector3.Lerp(start, target, k);
                yield return null;
            }

            _moveCoroutine = null;
        }
    }
}