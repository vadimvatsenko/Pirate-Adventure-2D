using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerFolder
{
    [CreateAssetMenu(fileName = "NewAnimationClip", menuName = "Animation/Clip")]
    public class AnimationClip : ScriptableObject
    {
        [SerializeField] private AnimationType animationType;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private int frameRate;
        [SerializeField] private bool loop;
        [SerializeField] private bool allowNextClip;
        [SerializeField] private UnityEvent onComplete;

        public AnimationType AnimationType => animationType;
        public List<Sprite> Sprites => sprites;
        public bool Loop => loop;
        public int FrameRate => frameRate;
        public bool AllowNextClip => allowNextClip;
        public UnityEvent OnComplete => onComplete;
    }
}