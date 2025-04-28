using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HiddenDoor : MonoBehaviour
    {
        [SerializeField] private float durationTime = 0.05f;
        [SerializeField] private Transform[] waypoints;
        
        private Vector3[] _waypointPositions;
        
        private float _elapsedTime;
        private bool _isMoving = false;
        private bool _isOpen = false;
        
        public UnityEvent OnEnableDoor;
            
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
            if(_isMoving) return;
            
            OnEnableDoor?.Invoke();
            
            StartCoroutine(ToggleDoorCoroutine());
        }

        private IEnumerator ToggleDoorCoroutine()
        {
            _isMoving = true;
            _elapsedTime = 0f;
            
            while (durationTime > _elapsedTime)
            {
                float t = _elapsedTime / durationTime;
                
                transform.position = Vector3.Lerp(_waypointPositions[0], _waypointPositions[1], t);
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = _waypointPositions[1];
            _isOpen = !_isOpen;
            _isMoving = false;
            
            
            SwapWayPoints();
        }

        private void SwapWayPoints()
        {
            var temp = _waypointPositions[1];
            _waypointPositions[1] = _waypointPositions[0];
            _waypointPositions[0] = temp;
        }
    }
}