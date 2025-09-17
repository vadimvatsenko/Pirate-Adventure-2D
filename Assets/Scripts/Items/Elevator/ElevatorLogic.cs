using UnityEngine;

namespace Items.Elevator
{
    public class ElevatorLogic : MonoBehaviour
    {
        [SerializeField] private Transform[] elevatorPoints;
        [SerializeField] private float travelTime = 2f;

        private Vector3 _startPosition;
        private float _travelTimer;
        private Vector2[] _worldElevatorPoints;

        private void Start()
        {
            _startPosition = transform.position;
            _travelTimer = 0;
            _worldElevatorPoints = new Vector2[elevatorPoints.Length];

            for (int i = 0; i < _worldElevatorPoints.Length; i++)
            {
                _worldElevatorPoints[i] = new Vector2(elevatorPoints[i].position.x, elevatorPoints[i].position.y);
            }
        }

        private void Update()
        {
            if (_travelTimer < travelTime)
            {
                _travelTimer += Time.deltaTime;
                
                float t = _travelTimer / travelTime;
                transform.position = Vector2.Lerp(_startPosition, _worldElevatorPoints[1], t);
                
            }
        }
    }
}