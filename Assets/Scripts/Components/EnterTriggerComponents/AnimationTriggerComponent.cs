using Animation.EditorHelpers;
using Components.EnterCollisionComponents;
using UnityEngine;

namespace Components.EnterTriggerComponents
{
    public class AnimationTriggerComponent : BaseEnterTriggerComponent
    {
        [AnimationName] [SerializeField] private string nextAnimationName;
        [SerializeField] private EnterAnimationEvent onEnterAnimation;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            onEnterAnimation?.Invoke(nextAnimationName);
        }
    }
}