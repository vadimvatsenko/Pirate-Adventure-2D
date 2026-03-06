using System;
using Animation;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.CreaturesStateMachine.Player;
using UnityEditorInternal;
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
        [Space]
        [Header("Hero Detection")] 
        [SerializeField] private float radius;

        private bool _isHeroDetection = false;
        
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
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        private Collider2D _collider;
        public Collider2D Collider => _collider;
        
        private BasicStateMachine _stateMachine;
        private BasicState _disappearState;
        private BasicState _chaseState;
        private BasicState _idleState;
        private BasicState _appearState;
        private BasicState _hitState;
        private BasicState _moveState;
        
        public BasicState ChaseState => _chaseState;
        
        
        
        private Transform _playerTransform;

        private void Awake()
        {
            _stateMachine = new BasicStateMachine();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            
            _idleState = new GhostIdleState(this, _stateMachine, AnimatorHashes.Idle);
            _appearState = new AppearState(this, _stateMachine, AnimatorHashes.Appear);
            _disappearState = new DisappearState(this, _stateMachine, AnimatorHashes.Disappear);
            _hitState = new GhostBaseState(this, _stateMachine, AnimatorHashes.Hit);
            _moveState = new GhostMoveState(this, _stateMachine, AnimatorHashes.Hit);
            _chaseState = new GhostMoveState(this, _stateMachine, AnimatorHashes.Idle);
            _chaseState = new GhostMoveState(this, _stateMachine, AnimatorHashes.Appear);
            
            _collider = GetComponent<BoxCollider2D>();
            
            _stateMachine.Initialize(_idleState);
        }
        public void Update()
        {
            _stateMachine.UpdateActiveState();
            
            _isHeroDetection = Physics2D.CircleCast(
                transform.position, radius, Vector2.zero, 0, LayerMask.GetMask("Player"));

            if (_isHeroDetection)
            {
                HandleMovement();
            }
            
            
        }
        public Hero GetHero() => FindObjectOfType<Hero>();

        public void HandleMovement()
        {
            Vector3 playerTransform = GetHero().transform.position;
            
            transform.position = Vector2.MoveTowards(transform.position, playerTransform, 2 * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isHeroDetection ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}