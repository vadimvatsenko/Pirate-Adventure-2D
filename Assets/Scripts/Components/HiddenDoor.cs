using System.Collections;
using UnityEngine;

namespace Components
{
    public class HiddenDoor : MonoBehaviour
    {
        [SerializeField] private bool isOpen = false;
        
        [SerializeField] private float step = 2f;
        [SerializeField] private float durationTime = 0.5f;
        private float _elapsedTime;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private bool _isMoving = true;

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void ToogleDoor()
        {
            _isMoving = true;
            _elapsedTime = 0;
            _endPosition = isOpen
                ? _startPosition
                : new Vector3(transform.position.x, transform.position.y - step, transform.position.z);

            if (_isMoving)
            {
                StartCoroutine(ToogleDoorCoroutine());
            }
            isOpen = !isOpen;
        }

        private IEnumerator ToogleDoorCoroutine()
        {
            Debug.Log("Interact Door");
            while (durationTime > _elapsedTime)
            {
                float t = _elapsedTime / durationTime;
                transform.position = Vector3.Lerp(_startPosition, _endPosition, t);
                _elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            transform.position = _endPosition;
        }
    }
}