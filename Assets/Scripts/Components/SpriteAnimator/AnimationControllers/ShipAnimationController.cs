using System;
using System.Linq;
using Components.SpriteAnimator.AnimationTypes;
using Items;
using PlayerFolder;
using UnityEngine;

namespace Components.SpriteAnimator.AnimationControllers
{
    public class ShipAnimationController : MonoBehaviour
    {
        [SerializeField] private HandleAnimationClip[] animationClip;
        private HandleSpriteAnimator _handleSpriteAnimator;
        private Sail _sail;
        private string _curentPlayerAnimationType;
        
        private void Awake()
        {
            _sail = GetComponent<Sail>();
            _handleSpriteAnimator = GetComponent<HandleSpriteAnimator>();
            
            var newClip = animationClip.FirstOrDefault(a => a.AnimationName == ShipAnimationType.Idle.ToString());
            _handleSpriteAnimator.SetAnimationClip(newClip);
        }

        private void Update()
        {
            string newPlayerAnimationType = GetAnimationType();

            if (newPlayerAnimationType != _curentPlayerAnimationType)
            {
                _curentPlayerAnimationType = newPlayerAnimationType;
            }
        }

        private string GetAnimationType()
        {
            return String.Empty;
        }
    }
}