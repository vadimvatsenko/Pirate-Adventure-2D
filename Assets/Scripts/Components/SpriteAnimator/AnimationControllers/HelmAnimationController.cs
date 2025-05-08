﻿using System.Linq;
using Components.SpriteAnimator.AnimationTypes;
using PlayerFolder;
using UnityEngine;

namespace Components.SpriteAnimator.AnimationControllers
{
    public class HelmAnimationController : MonoBehaviour
    {
        [SerializeField] private HandleAnimationClip[] animationClip;
        private HandleSpriteAnimator _spriteAnimator;

        private void Awake()
        {
            _spriteAnimator = GetComponent<HandleSpriteAnimator>();
            
            _spriteAnimator.SetAnimationClip(animationClip[0]);
            _spriteAnimator.PlayAnimation();
        }

        public void HelmTurn()
        {
            _spriteAnimator.SetAnimationClip(animationClip[1]);
            _spriteAnimator.PlayAnimation();
        }

        public void HelmIdle()
        {
            _spriteAnimator.SetAnimationClip(animationClip[0]);
            _spriteAnimator.PlayAnimation();
        }
    }
}