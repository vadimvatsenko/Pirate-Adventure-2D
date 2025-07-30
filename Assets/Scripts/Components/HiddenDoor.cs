using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HiddenDoor : MonoBehaviour
    {
        [SerializeField] private float durationTime = 0.05f;
        [SerializeField] private float delayTime = 0.5f;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private GameObject[] doorsElements;

        [Header("Events")] 
        public UnityEvent onEnableDoor;
        public UnityEvent onDoorOpened;
        public UnityEvent onDoorClosed;

        private Vector3[] _waypointPositions;
        private float _elapsedTime;
        private bool _isMoving = false;
        private bool _isOpen = false;

        private void Start()
        {
            UpdateWayPointsInfo();
            transform.position = _waypointPositions[0];
        }

        private void UpdateWayPointsInfo()
        {
            _waypointPositions = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                _waypointPositions[i] = waypoints[i].position;
            }
        }

        public void ToggleDoor()
        {
            if (_isMoving) return;

            if (_isOpen)
                CloseDoor();
            else
                OpenDoor();
        }

        public void OpenDoor()
        {
            if (_isMoving || _isOpen) return;

            onEnableDoor?.Invoke();
            StartCoroutine(MoveDoorCoroutine(_waypointPositions[0], _waypointPositions[1], true));
        }

        public void CloseDoor()
        {
            if (_isMoving || !_isOpen) return;

            onEnableDoor?.Invoke();
            StartCoroutine(MoveDoorCoroutine(_waypointPositions[1], _waypointPositions[0], false));
        }

        private IEnumerator MoveDoorCoroutine(Vector3 from, Vector3 to, bool opening)
        {
            yield return new WaitForSeconds(delayTime);
            _isMoving = true;
            _elapsedTime = 0f;

            for (int i = 0; i < doorsElements.Length; i++)
            {
                Vector3 fromCorrected = doorsElements[i].transform.position;
                Vector2 toCorrect = new Vector2(doorsElements[i].transform.position.x, to.y);
                _elapsedTime = 0f;
                
                while (_elapsedTime < durationTime)
                {
                    float t = _elapsedTime / durationTime;
                    
                    doorsElements[i].transform.position = Vector3.Lerp(fromCorrected, toCorrect, t);
                    _elapsedTime += Time.deltaTime;
                    yield return null;
                }

                doorsElements[i].transform.position = toCorrect;
                _isOpen = opening;
                _isMoving = false;
                
            }
            
            if (_isOpen)
                onDoorOpened?.Invoke();
            else
                onDoorClosed?.Invoke();
        }
    }
}
