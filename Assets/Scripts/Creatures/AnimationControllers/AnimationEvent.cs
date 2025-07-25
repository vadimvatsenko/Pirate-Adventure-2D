using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.AnimationControllers
{
    public class AnimationEvent : MonoBehaviour
    {
        public event UnityAction OnAnimationStart;
        public event UnityAction OnAnimationEnd;
        
        public void CallOnAnimationStart() => OnAnimationStart?.Invoke();
        public void CallOnAnimationEnd() => OnAnimationEnd?.Invoke();
    }
}