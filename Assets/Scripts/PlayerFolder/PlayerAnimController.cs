using System;
using System.Collections;
using DefaultNamespace.Model;
using UnityEngine;

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
        [SerializeField] private RuntimeAnimatorController withoutArmor;
        [SerializeField] private RuntimeAnimatorController withArmor;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        
        public Animator PlayerAnimator { get; private set; }
        private Player _player;
        private PlayerCollisionInfo _playerCollisionInfo;
        private GameSession _gameSession; // ++
        public event Action OnIsArmed;
        
        // Colors
        private Color startColor = new Color(1f, 1f, 1f, 0f);
        private Color endColor = new Color(1f, 1f, 1f, 1f);
        
        private void Awake()
        {
            _player = GetComponent<Player>();
            PlayerAnimator = GetComponentInChildren<Animator>();
            _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
            _gameSession = FindObjectOfType<GameSession>();

            if (_gameSession != null) // ++
            {
                UpdateArmedState();
            }
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
            _gameSession.PlayerData.isArmed = !_gameSession.PlayerData.isArmed;
            UpdateArmedState();
            OnIsArmed?.Invoke();
        }

        private void UpdateArmedState()
        {
            PlayerAnimator.runtimeAnimatorController 
                = _gameSession.PlayerData.isArmed ? withArmor : withoutArmor;
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