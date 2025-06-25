using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class Creature : MonoBehaviour
    {
        // Settings

        [Header("Movement info")] 
        [SerializeField] private float movementSpeed = 1.5f;
        
        [Space]
        [Header("Jump Info")]
        [SerializeField] protected float jumpForce;
        
        // Components
        public Animator AnimController { get; private set; }
        public CreatureCollisionInfo CollisionInfo { get; private set; }
        public CreatureStateMachine StateMachine { get; protected set; }
        public Rigidbody2D Rb2D { get; protected set; }
        public Collider2D C2D { get; protected set; }
        
        // Directions
        public bool IsFacingRight { get; protected set; } = true;
        public int FacingDirection { get; protected set; } = 1;
        public float XInput { get; private set; }
        
        // States
        public CreatureIdleState IdleState { get; protected set; }
        public CreatureMoveState MoveState { get; protected set; }
        public CreatureJumpState JumpState { get; protected set; }
        
        
        
        protected virtual void Awake()
        {
            AnimController = GetComponentInChildren<Animator>();
            CollisionInfo = GetComponent<CreatureCollisionInfo>();
            StateMachine = new CreatureStateMachine();
        }

        protected virtual void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            C2D = GetComponent<Collider2D>();
        }

        protected virtual void Update()
        {
            CollisionInfo.HandleGroundCheck();
            CollisionInfo.HandleWallCheck();
            StateMachine.CurrentState.Update();
            
        }
        
        public void SetDirection(float dir) => XInput = dir;
        
        public virtual void HandleMovement()
        {
            Rb2D.velocity = new Vector2(XInput * movementSpeed, Rb2D.velocity.y);
        }
        
        public void HandleFlip()
        {
            if (Rb2D.velocity.x < 0 && IsFacingRight || Rb2D.velocity.x > 0 && !IsFacingRight)
            {
                Flip();
            }
        }
        
        private void Flip()
        {
            IsFacingRight = !IsFacingRight;
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
            
            Debug.Log(FacingDirection);
        }
    }
}