using Components.HealthComponentFolder;
using Creatures.AnimationControllers;
using Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Creatures.CreaturesStateMachine.Hero
{
    public class Hero : Creature
    {
        public GameSession GameSess { get; private set;}
        public NewInputSet NewInputSet { get; private set; }
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        public float DoubleJumpForce => doubleJumpForce;
        public bool CanDoubleJump { get; private set; }
        public bool IsAirBorn { get; private set; }

        [Header("Attack Info")] 
        [SerializeField] private int attackForce;
        
        protected override void Awake()
        {
            base.Awake();
            NewInputSet = new NewInputSet();
            GameSess = FindObjectOfType<GameSession>();
        }
        
        private void Start()
        {
            IdleState = new HeroIdleState(this, StateMachine, AnimatorHashes.Idle);
            MoveState = new HeroMoveState(this, StateMachine, AnimatorHashes.Move);
            JumpState = new HeroJumpState(this, StateMachine, AnimatorHashes.JumpFall);
            DoubleJumpState = new HeroDoubleJumpState(this, StateMachine, AnimatorHashes.JumpFall); 
            FallState = new HeroFallState(this, StateMachine, AnimatorHashes.JumpFall);
            DeadState = new HeroDieState(this, StateMachine, AnimatorHashes.Die);
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
        
    }
}
