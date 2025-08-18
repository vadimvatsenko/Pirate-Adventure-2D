using System.Collections;
using UnityEngine;

namespace Items.Traps
{
    public class TrapSaw : MonoBehaviour
    {
        [SerializeField] protected float speed = 5f;
        [SerializeField] protected float cooldown = 1f;
        [SerializeField] protected Transform[] waypoints;
        
        private SpriteRenderer _spriteRenderer;
        
        protected Vector3[] WaypointsPositions;
        protected bool CanMove = true;
        protected int WaypointIndex = 0;
        protected int MovementDirection = 1;
        protected float RotationAngle = 360f;

        private void Awake()
        {
            UpdateWaypointPositions();
        }
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void UpdateWaypointPositions()
        {
            WaypointsPositions = new Vector3[waypoints.Length];

            for (int i = 0; i < waypoints.Length; i++)
            {
                WaypointsPositions[i] = waypoints[i].position;
            }
        }

        private void Update()
        {
            if(!CanMove) return;
            
            SawMovement();
            Rotation();
        }

        protected virtual void SawMovement()
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                    WaypointsPositions[WaypointIndex], 
                    speed * Time.deltaTime);
            

            if (Vector3.Distance(transform.position, WaypointsPositions[WaypointIndex]) < 0.001f)
            {
                WaypointIndex++;
            }

            if (WaypointIndex >= waypoints.Length)
            {
                WaypointIndex = 0;
            }
        }

        protected virtual void Rotation()
        {
            transform.Rotate(0, 0f,  (RotationAngle * MovementDirection) * Time.deltaTime);
        }

        protected IEnumerator CoolDown(float delay)
        {
            CanMove = false;
            yield return new WaitForSeconds(delay);
            CanMove = true;
            
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            if (WaypointsPositions == null)
            {
                foreach (var wayPoint in waypoints)
                {
                    Gizmos.DrawSphere(wayPoint.position, 0.1f);
                }
            }
            else
            {
                foreach (var wayPoint in WaypointsPositions)
                {
                    Gizmos.DrawSphere(wayPoint, 0.1f);
                }
            }
        }
    }
}