using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpriteAnimator.NewSpriteAnimator
{
    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private bool loop;
        [SerializeField] private bool allowNextClip;
        [SerializeField] private UnityEvent onComplete;

        public string Name => name;
        public Sprite[] Sprites => sprites;
        public bool Loop => loop;
        public bool AllowNextClip => allowNextClip;
        public UnityEvent OnComplete => onComplete;
    }
}