using System;
using System.Linq;
using UnityEngine;

namespace PlayerFolder
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationClip[] animationClip;
        private SpriteAnimationStates _spriteAnimation;
        private PlayerS _player;
        private AnimationType _curentAnimationType;
        
        private void Awake()
        {
            _player = GetComponentInParent<PlayerS>();
            _spriteAnimation = GetComponent<SpriteAnimationStates>();
            
            var newClip = animationClip.FirstOrDefault(a => a.AnimationType == AnimationType.Player_IDLE);
            _spriteAnimation.SetAnimationClip(newClip);
        }

        private void Update()
        {
            AnimationType newAnimationType = GetAnimationType();

            if (newAnimationType != _curentAnimationType)
            {
                _curentAnimationType = newAnimationType;
                var newClip = animationClip.FirstOrDefault(a => a.AnimationType == newAnimationType);
                if (newClip != null)
                {
                    _spriteAnimation.SetAnimationClip(newClip);
                }
                else
                {
                    Debug.LogError($"AnimationClip '{newAnimationType}' не найден!");
                }
            }
        }

        private AnimationType GetAnimationType()
        {
            if (_player.Rb.velocity.y > 0.1f) return AnimationType.Player_JUMP;
            if (_player.Rb.velocity.y < -0.1f) return AnimationType.Player_FALL;
            if (_player.XInput != 0) return AnimationType.Player_MOVE;
            return AnimationType.Player_IDLE;
        }
    }
}