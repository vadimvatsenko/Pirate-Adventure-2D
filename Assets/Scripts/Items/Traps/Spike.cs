using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float speed;
    private float _travelDistance;
    private Vector2[] _wayPoints;
    private int _wayPointIndex;
    private bool _canMove = false;
    
    private Vector2 _colliderPosition;
    private Vector2 _colliderSize;

    private void Start()
    {
        _travelDistance = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        SetupWayPoints();
        
        float randomDelay = Random.Range(0f, _travelDistance);
        Invoke(nameof(ActivateTrap), randomDelay);
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void ActivateTrap() => _canMove = true;

    private void HandleMovement()
    {
        if(!_canMove) return;
        
        transform.position = 
            Vector2.MoveTowards(
                transform.position, _wayPoints[_wayPointIndex], speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _wayPoints[_wayPointIndex]) < 0.1f)
        {
            _wayPointIndex++;
            if (_wayPointIndex >= _wayPoints.Length)
            {
                _wayPointIndex = 0;
            }
        }
    }

    private void SetupWayPoints()
    {
        _wayPoints = new Vector2[2];
        
        float yOffset = _travelDistance / 2;
        
        _wayPoints[0] = (Vector2)transform.position + new Vector2(0, yOffset);
        _wayPoints[1] = (Vector2)transform.position - new Vector2(0, yOffset);
    }
}
