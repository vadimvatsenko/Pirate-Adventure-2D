using PlayerFolder;
using UnityEngine;

namespace Components.SpriteAnimator.AnimationControllers
{
    public class SimpleAnimationController : MonoBehaviour
    {
        
        [SerializeField] private bool playOnAwake = true;
        [SerializeField] private HandleAnimationClip handleAnimationClip;
        
        private HandleSpriteAnimator _spriteAnimator;

        private void Awake()
        {
            _spriteAnimator = GetComponent<HandleSpriteAnimator>();
            _spriteAnimator.SetAnimationClip(handleAnimationClip);
            
            if (playOnAwake)
            {
                _spriteAnimator.PlayAnimation();
            }
        }

        //[ContextMenu("Play")]
        public void Play()
        {
            _spriteAnimator.PlayAnimation();
        }
    }
}