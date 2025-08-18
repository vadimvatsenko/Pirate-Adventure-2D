using UnityEngine;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.CreatureBasic;

namespace Components
{
    public class FallingPlatformComponent : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private BoxCollider2D[] _colliders;

        [SerializeField] private float speed = 0.75f;
        [SerializeField] private float travelDistance;
        private Vector3[] _wayPoints;
        private int _wayPointIndex;
        private bool _canMove = false;

        [Header("Trap Falling Platform Details")] [SerializeField]
        private float impactSpeed = 3f;

        [SerializeField] private float impactDuration = 0.1f;
        private float _impactTimer;
        private bool _impactHappend;
        [Space] 
        [SerializeField] private float fallDelay = 0.5f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _colliders = GetComponents<BoxCollider2D>();
        }

        private void Start()
        {
            SetupWayPoints();

            // метод в котором будет создаватся рандомная стартовая точка для платформы,
            // в случай если у нас будет много платформ, чтобы они не выглядели одинаково
            float randomDelay = Random.Range(0f, travelDistance);
            Invoke(nameof(ActivatePlatform), randomDelay);
        }

        private void ActivatePlatform() => _canMove = true;

        private void SetupWayPoints()
        {
            _wayPoints = new Vector3[2]; // инициализируем 2 точки, по которым будет двигаться платформа

            float yOffset = travelDistance / 2; // половинка нашего пути

            _wayPoints[0] = transform.position + new Vector3(0, yOffset, 0); // точка 1, верх + половинка пути
            _wayPoints[1] = transform.position + new Vector3(0, -yOffset, 0); // точка 2, низ + половинка пути
            // для того, что бы платформа начала двигаться со свого положения
            // на половину пути вверх и них от начала своей позиции
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleImpact();
        }

        private void HandleMovement()
        {
            if (!_canMove) return;

            transform.position =
                Vector3.MoveTowards(
                    transform.position, _wayPoints[_wayPointIndex], speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, _wayPoints[_wayPointIndex]) < 0.1f)
            {
                _wayPointIndex++;
                if (_wayPointIndex >= _wayPoints.Length)
                {
                    _wayPointIndex = 0;
                }
            }
        }

        private void HandleImpact()
        {
            if (_impactTimer < 0) return;
            _impactTimer -= Time.fixedDeltaTime;

            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    transform.position + (Vector3.down * 3),
                    impactSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_impactHappend) return;
            Creature creaturesOld = other.GetComponent<Creature>();
            if (creaturesOld)
            {
                Invoke(nameof(SwitchOffPlatform), fallDelay); // вызывает метод с задержкой
                _impactTimer = impactDuration;
                _impactHappend = true;
            }
        }

        private void SwitchOffPlatform()
        {
            _canMove = false;
            _rb.isKinematic = false;
            _rb.gravityScale = 3.5f;

            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
            
            Destroy(gameObject, 2f);
        }
    }
}