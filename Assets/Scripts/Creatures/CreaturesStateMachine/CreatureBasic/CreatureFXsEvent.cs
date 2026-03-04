using Creatures.CreaturesCollisions;
using UnityEngine;
using UnityEngine.Events;

// система вызовов VFX и SFX
namespace Creatures.CreaturesStateMachine.CreatureBasic
{
    public class CreatureFXsEvent : MonoBehaviour
    {
        
        [SerializeField] protected UnityEvent OnMove;
        [SerializeField] protected UnityEvent onFall;
        [SerializeField] protected UnityEvent onHit;
        
        private Creature _creature;
        private BasicCollisionInfo _collisionInfo;
        
        // Партикал приземления
        [SerializeField] protected float MinJumpHeight = 8f;
        protected float CurrentJumpHeight;
        protected float MoveSpawnTimer = 0.25f;
        
        protected virtual void Awake()
        { 
            _creature = GetComponentInParent<Creature>();
            
            if (_creature != null)
            {
                _collisionInfo = _creature.GetComponent<BasicCollisionInfo>();
            }
        }
        
        protected virtual void Update()
        {
            HandleSpawnFallPartical();
            HandleSpawnMovementPartical();

            HandleTimer();
        }
        
        protected virtual void HandleSpawnMovementPartical()
        {
            if (_creature.XInput != 0 && MoveSpawnTimer <= 0 && _collisionInfo.IsGrounded)
            {
                OnMove?.Invoke();
                MoveSpawnTimer = 0.25f;
            }
        }

        private void HandleTimer()
        {
            MoveSpawnTimer -= Time.deltaTime;
        }
        
        protected virtual void HandleSpawnFallPartical()
        {
            float vel = Mathf.Abs(_creature.Rb2D.velocity.y);
            
            if (vel > MinJumpHeight)
            {
                CurrentJumpHeight = vel;
            }
            
            if (CurrentJumpHeight > MinJumpHeight && _collisionInfo.IsGrounded)
            {
                onFall?.Invoke();
                CurrentJumpHeight = 0;
            }
        }
    }
}