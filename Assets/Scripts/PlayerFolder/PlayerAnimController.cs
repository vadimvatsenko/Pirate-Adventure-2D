using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerFolder
{
    public class PlayerAnimController : MonoBehaviour
    {
        // что тут происходит, перевод string в hash
        private static int XVelocityKey { get; } = Animator.StringToHash("xVelocity");
        private static readonly int YVelocityKey = Animator.StringToHash("yVelocity");
        private static readonly int IsGroundedKey = Animator.StringToHash("isGrounded");
        private static readonly int Knockback = Animator.StringToHash("knockback");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int SitKey = Animator.StringToHash("sit");
        private static readonly int Die = Animator.StringToHash("die");
        
        [Header("Animator Controllers")]
        [SerializeField] private AnimatorController withoutArmor;
        [SerializeField] private AnimatorController withArmor;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        
        public Animator PlayerAnimator { get; private set; }
        private Player _player;
        private PlayerCollisionInfo _playerCollisionInfo;
        private bool _isArmed = false;
        
        // Colors
        private Color startColor = new Color(1f, 1f, 1f, 0f);
        private Color endColor = new Color(1f, 1f, 1f, 1f);
        
        public bool IsArmed => _isArmed;

        private void Awake()
        {
            _player = GetComponent<Player>();
            PlayerAnimator = GetComponentInChildren<Animator>();
            _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
        }

        private void Start()
        {
            playerSpriteRenderer.color = startColor;
            ShowPlayer();
        }

        private void ShowPlayer() => StartCoroutine(ShowPlayerCoroutine(startColor, endColor));
        public void HidePlayer(float duration) => StartCoroutine(ShowPlayerCoroutine(endColor, startColor, duration));

        private IEnumerator ShowPlayerCoroutine(Color col1, Color col2, float duration = 1)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                playerSpriteRenderer.color = 
                    Color.Lerp(col1, col2, elapsed / duration);
                yield return null;
            }
        }
        
        
        public void HandleAnimation()
        {
            Vector2 velocityNormalized = _player.Rb.velocity.normalized;
            PlayerAnimator.SetFloat(XVelocityKey, velocityNormalized.x);
            PlayerAnimator.SetFloat(YVelocityKey, velocityNormalized.y);
            PlayerAnimator.SetBool(IsGroundedKey, _playerCollisionInfo.IsGrounded);
        }
        
        public void ChangeArmedState()
        {
            _isArmed = !_isArmed;
            PlayerAnimator.runtimeAnimatorController = _isArmed ? withArmor : withoutArmor;
        }

        public void SetAttackAnimation()
        {
            PlayerAnimator.SetTrigger(AttackKey);
        }

        public void SetKnockbackAnimation()
        {
            PlayerAnimator.SetTrigger(Knockback);
        }

        public void SetSitAnimation()
        {
            PlayerAnimator.SetTrigger(SitKey);
        }

        public void SetDieAnimation()
        {
            PlayerAnimator.SetTrigger(Die);
        }
    }
}