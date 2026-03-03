using System;
using Animation;
using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player.PlayerStates;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Player
{
    public class Hero : Creature
    {
        public GameSession GameSess { get; private set;} //
        public NewInputSet NewInputSet { get; private set; }
        public Animator HeroAnimator { get; private set; }
        public BaseHealthComponent HealthComponent { get; private set; }
        public HeroCollisionInfo HeroCollision { get; private set; }
        public HeroStatesController HeroStatesController { get; private set; }

        [Header("Jump Counter")]
        [SerializeField] private int maxJumpCounter = 2;
        public int JumpCounter { get; set;}
        
        [Header("Double Jump")] 
        [SerializeField] private float _doubleJumpForce = 2f;
        public float DoubleJumpForce => _doubleJumpForce;
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
            HealthComponent =  GetComponent<PlayerHealthComponent>();
            
            // подписка на изменения в инвентаре
            GameSess.PlayerData.InventoryData.OnChanged += OnInventoryChanged;
        }
        
        private void Start()
        {
            IdleState = new HeroIdleState(this, StateMachine, AnimatorHashes.Idle);
            MoveState = new HeroMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new HeroJumpState(this, StateMachine, AnimatorHashes.JumpFall);
            AttackState = new HeroAttackState(this, StateMachine, AnimatorHashes.Attack);
            FallState = new HeroFallState(this, StateMachine, AnimatorHashes.JumpFall);
            DeathState = new HeroDeathState(this, StateMachine, AnimatorHashes.Death);
            HitState = new HeroHitState(this, StateMachine, AnimatorHashes.Hit);
            ThrowState = new HeroThrowState(this, StateMachine, AnimatorHashes.Throw);
            
            HeroStatesController = 
                new HeroStatesController(this, StateMachine, NewInputSet, GameSess, HeroAnimator);
                
            StateMachine.Initialize(IdleState);
        }

        private void OnEnable()
        {
            NewInputSet.Enable();
            // включение системы управления
            NewInputSet.Hero.Movement.performed += context => XInput = context.ReadValue<Vector2>().x;
            NewInputSet.Hero.Movement.canceled += context => XInput = 0;
            NewInputSet.Hero.UseHealthPoison.performed += context => UsePoison(context);
        }
        
        private void OnDisable()
        {
            NewInputSet.Disable();
            NewInputSet?.Dispose();
            
            NewInputSet.Hero.UseHealthPoison.performed -= context => UsePoison(context);
            
            NewInputSet.Hero.Movement.performed -= context => XInput = context.ReadValue<Vector2>().x;
            NewInputSet.Hero.Movement.canceled -= context => XInput = 0;
            
            GameSess.PlayerData.InventoryData.OnChanged -= OnInventoryChanged;
        }
        
        protected override void Update()
        {
            base.Update();
            Debug.Log(JumpCounter);
            HandleFlip();
        }

        public void AddInInventory(string id, int count)
        {
            GameSess.PlayerData.InventoryData.Add(id, count);
        }
        
        private void OnInventoryChanged(string id, int value)
        {
            // пока пустой
            //Debug.Log($"id: {id}, value: {value}");
        }

        private void UsePoison(InputAction.CallbackContext context)
        {
            bool isHealth = GameSess.PlayerData.health < GameSess.PlayerData.maxHealth;
            Debug.Log(isHealth);
            
            if (isHealth)
            {
                var poisonCount = GameSess.PlayerData.InventoryData.Count("HealthPoison");
                
                if (poisonCount > 0)
                {
                    HealthComponent.TakeHeal(15);
                    GameSess.PlayerData.InventoryData.Remove("HealthPoison", 1);
                }
            }
        }
    }
}
