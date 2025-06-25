using Components.HealthComponentFolder;
using Creatures.CreaturesStateMachine;
using Model;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures
{
    public class Hero : Creature, IMovable
    {
        private GameSession _gameSession;
        
        [Header("DoubleJump Info")] 
        [SerializeField] private float doubleJumpForce;
        private bool _canDoubleJump;
        private bool _isPressedJumpButton;
        
        public UnityEvent onPlayerTakeDamage;
        public UnityEvent onPlayerDeath;
        public UnityEvent OnSpawnObject;
        
        public bool CanDoubleJump => _canDoubleJump;
        public bool IsPressedJumpButton => _isPressedJumpButton;
        
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        
        /*protected override void HandleLanding()
        {
            base.HandleLanding();
            _canDoubleJump = true;
        }
        
        public void HandleJump(bool isPressedSpace)
        {
            _isPressedJumpButton = isPressedSpace;
            if (_isPressedJumpButton)
            {
                
                if (CollisionInfo.IsGrounded)
                {
                    CallEventOnCreatureJump(); // событие
                    Rb.AddForce(new Vector2(Rb.velocity.x, jumpForce), ForceMode2D.Impulse);
                }

                if (IsAirborne && _canDoubleJump)
                {
                    CallEventOnCreatureJump(); // событие
                    HandleDoubleJump();
                }
            }

            else if (Rb.velocity.y > 0) // уменьшаем прыжок, если кнопка не нажата.
            {
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y * 0.5f);
            }
        }

        private void HandleDoubleJump()
        {
            _canDoubleJump = false;
            Rb.velocity = new Vector2(Rb.velocity.x, doubleJumpForce);
        }
        
        public override void Attack()
        {
            if (!_gameSession.PlayerData.isArmed || !CollisionInfo.IsGrounded) return;

            CallEventOnCreatureAttack(); //событие
            CreatureAnimationController.SetAttackAnimation();
            var gos = CollisionInfo.GetObjectsInRange();
            
            foreach (var go in gos)
            {
                var hp = go.GetComponent<IHealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(attackPower);
                }
            }
        }*/
    }
}
