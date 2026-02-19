using Animation;
using Creatures.AnimationControllers;
using UnityEngine;

namespace Items.Traps.Spikes
{
    public class SpikesController : MonoBehaviour
    {
        [SerializeField] private SpriteAnimator[] spikes;
        public void ActivateSpikes()
        {
            foreach (var spike in spikes)
            {
                spike.SetClip(AnimatorHashes.GetName(AnimatorHashes.Attack));
            }
        }
    }
}