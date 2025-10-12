using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using GameManagerInfo;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player
{
    public class Hero : Creature
    {
        public GameSession GameSess { get; private set;} //
        public NewInputSet NewInputSet { get; private set; }
        public Animator HeroAnimator { get; private set; }
        
        public HeroCollisionInfo HeroCollision { get; private set; }
        private HeroStatesController _heroStatesController;
        
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

        
        protected override void Awake()
        {
            base.Awake();
            NewInputSet = new NewInputSet();
            GameSess = FindObjectOfType<GameSession>();
            HeroCollision = GetComponent<HeroCollisionInfo>();
            HeroAnimator = GetComponentInChildren<Animator>();
            
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
            ThrowState = new HeroThrowState(this, StateMachine, AnimatorHashes.Throw); // ++
                
            StateMachine.Initialize(IdleState);
            
            _heroStatesController 
                = new HeroStatesController(this, StateMachine, NewInputSet, GameSess, HeroAnimator);
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
            HandleFlip();
        }
    }
}
