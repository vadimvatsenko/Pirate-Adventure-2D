using Animation;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class Ghost : Creature
    {
        [Header("Ghost Details")] 
        [SerializeField] private float activeDuration;
        [SerializeField] private float idleDuration;
        [Space]
        [SerializeField] private float xMinDistance;
        [SerializeField] private float yMinDistance;
        [SerializeField] private float yMaxDistance;
        
        private VisionComponent _vision;
        public VisionComponent Vision => _vision;
        
        
        private bool _isHeroDetection;
        public bool IsHeroDetection => _isHeroDetection;
        
        public float XMinDistance => xMinDistance;
        public float YMinDistance => yMinDistance;
        public float YMaxDistance => yMaxDistance;

        public float ActiveDuration
        {
            get => activeDuration;
            set => activeDuration = value;
        }

        public float IdleDuration
        {
            get => idleDuration;
            set => idleDuration = value;
        }
        
        private float _activeTimer;
        private float _idleTimer;
        
        private bool _isChaising;

        public float ActiveTimer
        {
            get => _activeTimer;
            set => _activeTimer = value;
        }

        public float IdleTimer
        {
            get => _idleTimer;
            set => _idleTimer = value;
        }
        public bool IsChaising
        {
            get => _isChaising;
            set => _isChaising = value;
        }

        private SpriteRenderer _spriteRenderer;
        
        private BasicState _disappearState;
        private BasicState _chaseState;
        private BasicState _appearState;
        private BasicState _invisibleState;
        
        // properties
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public BasicState AppearState => _appearState;
        public BasicState InvisibleState => _invisibleState;
        public BasicState DisappearState => _disappearState;
        public BasicState ChaseState => _chaseState;
        
        private Transform _playerTransform;

        protected override void Awake()
        {
            base.Awake();
            
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _vision = GetComponent<VisionComponent>();
            
            IdleState = new GhostIdleState(this, StateMachine, AnimatorHashes.Idle);
            
            _invisibleState = new GhostInvisibleState(this, StateMachine, AnimatorHashes.Idle);
            _appearState = new GhostAppearState(this, StateMachine, AnimatorHashes.Appear);
            _disappearState = new GhostDisappearState(this, StateMachine, AnimatorHashes.Disappear);
            _chaseState = new GhostChaseState(this, StateMachine, AnimatorHashes.Idle);
            DeathState = new GhostDeathState(this, StateMachine, AnimatorHashes.Idle);
            
            StateMachine.Initialize(_invisibleState);
        }
        protected override void Update()
        {
            StateMachine.CurrentState.Update();
            
        }
        public Hero GetHero() => FindObjectOfType<Hero>();

        public override void HandleMovement()
        {
            Hero hero = GetHero();

            if (hero != null)
            {
                int needFlip = (this.transform.position.x < hero.transform.position.x) ? 1 : -1;
                
                if (needFlip != FacingDirection)
                {
                    Flip();
                }
                transform.position 
                    = Vector2.MoveTowards(
                        transform.position, 
                        new Vector2(hero.transform.position.x - 0.5f,  hero.transform.position.y - 0.5f), 
                        MovementSpeed * Time.deltaTime);
            }
        }
        
        public void HandleAppearState() => StateMachine.ChangeState(AppearState);
        public void HandleDisappearState() => StateMachine.ChangeState(_disappearState);
        public void HandleInvisibleState() => StateMachine.ChangeState(_invisibleState);
        
        
    }
}