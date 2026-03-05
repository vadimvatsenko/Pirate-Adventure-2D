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

        private SpriteRenderer _spriteRenderer;
        
        private BasicState _disappearState;
        private BasicState _appearState;
        
        private float _activeTimer;
        private float _idleTimer;
        
        private bool _isChaising;
        
        private Transform _playerTransform;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _appearState = new AppearState(this, StateMachine, AnimatorHashes.Appear);
            _disappearState = new DisappearState(this, StateMachine, AnimatorHashes.Appear);
            HitState = new GhostBaseState(this, StateMachine, AnimatorHashes.Hit);
            MoveState = new GhostMoveState(this, StateMachine, AnimatorHashes.Hit);
            
            StateMachine.Initialize(_disappearState);
        }
        public void Update()
        {
            _activeTimer -= Time.deltaTime;

            if (!_isChaising && _idleTimer < 0)
            {
                StartChase();
            }
            else if (_isChaising && _activeTimer < 0)
            {
                EndChase();
            }
        }

        private void StartChase()
        {
            Transform heroTransform = FindObjectOfType<Hero>().transform;

            if (heroTransform == null)
            {
                EndChase();
                return;
            }

            float xOffset = Random.Range(0, 100) < 50 ? -1 : 1;
            
            float yPos = Random.Range(yMinDistance, yMaxDistance);
            
            transform.position = heroTransform.position + new Vector3(xMinDistance * xOffset, yPos, 0);
            
            _activeTimer = activeDuration;
            _isChaising = true;
        }

        private void EndChase()
        {
            _idleTimer = idleDuration;
            _isChaising = false;
        }
        
        public void MakeInvisible() => _spriteRenderer.color = Color.clear;
        public void MakeVisible() => _spriteRenderer.color = Color.white;
    }
}