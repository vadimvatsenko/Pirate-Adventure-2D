using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.Settings;
using GameManagerInfo;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class Hero : Creature
    {
        public GameSession GameSess { get; private set;}
        public NewInputSet NewInputSet { get; private set; }
        public GameManager GameMg { get; private set; }
        
        public HeroCollisionInfo HeroCollision { get; private set; }
        
        [Header("Buffer Jump")] 
        [SerializeField] private float bufferJumpWindow = 0.25f;
        private float _bufferJumpActivated = -1;
        public float BufferJumpWindow => bufferJumpWindow;
        public float BufferJumpActivated => _bufferJumpActivated;
        
        [Header("Coyote Jump")] 
        [SerializeField] private float coyoteJumpWindow = 0.5f; // Окно буфера (сколько секунд допустимо)
        private float _coyoteJumpActivated = -1; 
        public float CoyoteJumpWindow => coyoteJumpWindow;
        public float CoyoteJumpActivated => _coyoteJumpActivated;
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        public float DoubleJumpForce => doubleJumpForce;
        public bool CanDoubleJump { get; private set; }
        public bool IsAirBorn { get; private set; }

        [Header("Attack Info")] 
        [SerializeField] private int attackForce = 1;
        public int AttackForce => attackForce;
        
        [Header("Climb Info")]
        [SerializeField] private BoxCollider2D[] climbingBoxes;
        public BoxCollider2D ClimbingBox => climbingBoxes[0];
        
        protected override void Awake()
        {
            base.Awake();
            NewInputSet = new NewInputSet();
            GameSess = FindObjectOfType<GameSession>();
            GameMg = FindObjectOfType<GameManager>();
            HeroCollision = GetComponent<HeroCollisionInfo>();
        }
        
        private void Start()
        {
            IdleState = new HeroIdleState(this, StateMachine, AnimatorHashes.Idle);
            MoveState = new HeroMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new HeroJumpState(this, StateMachine, AnimatorHashes.JumpFall);
            DoubleJumpState = new HeroDoubleJumpState(this, StateMachine, AnimatorHashes.JumpFall);
            AttackState = new HeroAttackState(this, StateMachine, AnimatorHashes.Attack);
            FallState = new HeroFallState(this, StateMachine, AnimatorHashes.JumpFall);
            DeathState = new HeroDeathState(this, StateMachine, AnimatorHashes.Death);
            HitState = new HeroHitState(this, StateMachine, AnimatorHashes.Hit);
            ClimbState = new HeroClimbState(this, StateMachine, AnimatorHashes.Climb);
                
            StateMachine.Initialize(IdleState);
        }

        private void OnEnable()
        {
            NewInputSet.Enable(); // включение системы управления
            
            NewInputSet.Hero.Movement.performed += context => XInput = context.ReadValue<Vector2>().x;
            NewInputSet.Hero.Movement.canceled += context => XInput = 0;
        }

        private void OnDisable()
        {
            NewInputSet.Disable(); // выключение системы управления
        }

        protected override void Update()
        {
            base.Update();
            //UpdateAirBornStatus();
            
            HandleFlip();
        }

        private void FixedUpdate()
        {
            HeroCollision.CheckHeroGrab();
            //Debug.Log(HeroCollision.IsGrabb);
        }
        
        
        /*public void HandleJump(bool isPressedSpace)
        {
            _isPressedJumpButton = isPressedSpace;
            if (_isPressedJumpButton)
            {
                if (CollisionInfo.IsGrounded)
                {
                    CallOnJumpEvent(); // событие
                    StateMachine.ChangeState(HeroJumpFallState);
                }

                if (IsAirBorn && CanDoubleJump)
                {
                    CallOnJumpEvent();
                    HandleDoubleJump();
                }
            }

            else if (Rb2D.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
            {
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, Rb2D.velocity.y * 0.5f);
            }
        }

        private void HandleDoubleJump()
        {
            CanDoubleJump = false;
        }*/
        
    }
}
