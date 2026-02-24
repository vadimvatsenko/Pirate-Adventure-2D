using Animation.EditorHelpers;
using Components.EnterCollisionComponents;
using UnityEngine;

namespace Components.EnterTriggerComponents
{
    public class AnimationTriggerComponent : BaseEnterTriggerComponent
    {
        [AnimationName] [SerializeField] protected string nextAnimationName;
        [SerializeField] protected EnterAnimationEvent onEnterAnimation;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObjectTag.Equals(other.gameObject.tag))
            {
                onAction?.Invoke(other.gameObject);
                onEnter?.Invoke();
                onEnterAnimation?.Invoke(nextAnimationName);
            }
        }
    }
}