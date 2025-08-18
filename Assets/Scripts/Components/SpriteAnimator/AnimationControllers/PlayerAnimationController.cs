using System.Linq;
using Components.SpriteAnimator.AnimationTypes;
using Creatures;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.CreatureBasic;
using PlayerFolder;
using UnityEngine;

namespace Components.SpriteAnimator.AnimationControllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private HandleAnimationClip[] animationClip;
        private HandleSpriteAnimator _handleSpriteAnimator;
        private Creature _creature;
        private string _curentPlayerAnimationType;
        
        private void Awake()
        {
            _creature = GetComponentInParent<Creature>();
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
            if (_creature.Rb2D.velocity.y > 0.1f) return PlayerAnimationType.PlayerJump.ToString();
            if (_creature.Rb2D.velocity.y < -0.1f) return PlayerAnimationType.PlayerFall.ToString();
            if (_creature.XInput != 0) return PlayerAnimationType.PlayerMove.ToString();
            return PlayerAnimationType.PlayerIdle.ToString();
        }
    }
}