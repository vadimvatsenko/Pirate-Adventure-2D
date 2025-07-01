using Components.HealthComponentFolder;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class Hero : Creature
    {
        private GameSession _gameSession;
        public NewInputSet NewInputSet { get; private set; }
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        public float DoubleJumpForce => doubleJumpForce;
        public bool CanDoubleJump { get; private set; }
        public bool IsAirBorn { get; private set; }

        [Header("Attack Info")] 
        [SerializeField] private int attackForce;
        
        // Animation Controllers
        [Header("Animator Controllers")]
        [SerializeField] private RuntimeAnimatorController withoutArmor;
        [SerializeField] private RuntimeAnimatorController withArmor;
        public RuntimeAnimatorController WithoutArmor => withoutArmor;
        public RuntimeAnimatorController WithArmor => withArmor;
        
        // States
        public HeroIdleState HeroIdleState { get; private set; }
        public HeroMoveState HeroMoveState { get; private set; }
        public HeroJumpState HeroJumpState { get; private set; }
        public HeroDoubleJumpState HeroDoubleJumpState { get; private set; }
        public HeroFallState HeroFallState { get; private set; }
        public HeroDieState HeroDieState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            NewInputSet = new NewInputSet();
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        private void Start()
        {
            HeroIdleState = new HeroIdleState(this, StateMachine, AnimatorHashes.Idle);
            HeroMoveState = new HeroMoveState(this, StateMachine, AnimatorHashes.Move);
            HeroJumpState = new HeroJumpState(this, StateMachine, AnimatorHashes.JumpFall);
            HeroDoubleJumpState = new HeroDoubleJumpState(this, StateMachine, AnimatorHashes.JumpFall); 
            HeroFallState = new HeroFallState(this, StateMachine, AnimatorHashes.JumpFall);
            HeroDieState = new HeroDieState(this, StateMachine, AnimatorHashes.Die);
            
            StateMachine.Initialize(HeroIdleState);
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
            
            //Debug.Log(Rb2D.velocity);
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
        
        public void Attack()
        {
            if (!_gameSession.PlayerData.isArmed || !CollisionInfo.IsGrounded) return;
            
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(attackForce);
                }
            }
        }
    }
}
