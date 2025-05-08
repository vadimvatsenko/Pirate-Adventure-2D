using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Components.SpriteAnimator
{
    [CreateAssetMenu(fileName = "NewAnimationClip", menuName = "Animation/Clip")]
    public class HandleAnimationClip : ScriptableObject
    {
        [SerializeField] private string animationName;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private int frameRate;
        [SerializeField] private bool loop;
        [SerializeField] private bool allowNextClip;
        //[SerializeField] private UnityEvent onComplete;

        public string AnimationName => animationName;
        public List<Sprite> Sprites => sprites;
        public bool Loop => loop;
        public int FrameRate => frameRate;
        public bool AllowNextClip => allowNextClip;
        //public UnityEvent OnComplete => onComplete;
    }
}