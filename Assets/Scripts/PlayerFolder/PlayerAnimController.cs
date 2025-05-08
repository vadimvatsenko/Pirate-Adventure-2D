using UnityEditor.Animations;
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
        
        [Header("Animator Controllers")]
        [SerializeField] private AnimatorController withoutArmor;
        [SerializeField] private AnimatorController withArmor;
        
        public Animator PlayerAnimator { get; private set; }
        private Player _player;
        private PlayerCollisionInfo _playerCollisionInfo;
        private bool _isArmed = false;
        
        public bool IsArmed => _isArmed;

        private void Awake()
        {
            _player = GetComponent<Player>();
            PlayerAnimator = GetComponentInChildren<Animator>();
            _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
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
    }
}