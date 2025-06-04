using System.Linq;
using Components.SpriteAnimator.AnimationTypes;
using PlayerFolder;
using UnityEngine;

namespace Components.SpriteAnimator.AnimationControllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private HandleAnimationClip[] animationClip;
        private HandleSpriteAnimator _handleSpriteAnimator;
        private Hero _hero;
        private string _curentPlayerAnimationType;
        
        private void Awake()
        {
            _hero = GetComponentInParent<Hero>();
            _handleSpriteAnimator = GetComponent<HandleSpriteAnimator>();
            
            var newClip = animationClip.FirstOrDefault(a => a.AnimationName == PlayerAnimationType.PlayerIdle.ToString());
            _handleSpriteAnimator.SetAnimationClip(newClip);
        }

        private void Update()
        {
            string newPlayerAnimationType = GetAnimationType();

            if (newPlayerAnimationType != _curentPlayerAnimationType)
            {
                _curentPlayerAnimationType = newPlayerAnimationType;
                var newClip = animationClip.FirstOrDefault(a => a.AnimationName == newPlayerAnimationType);
                if (newClip != null)
                {
                    _handleSpriteAnimator.SetAnimationClip(newClip);
                }
                else
                {
                    Debug.LogError($"AnimationClip '{newPlayerAnimationType}' не найден!");
                }
            }
        }

        private string GetAnimationType()
        {
            if (_hero.Rb.velocity.y > 0.1f) return PlayerAnimationType.PlayerJump.ToString();
            if (_hero.Rb.velocity.y < -0.1f) return PlayerAnimationType.PlayerFall.ToString();
            if (_hero.XInput != 0) return PlayerAnimationType.PlayerMove.ToString();
            return PlayerAnimationType.PlayerIdle.ToString();
        }
    }
}