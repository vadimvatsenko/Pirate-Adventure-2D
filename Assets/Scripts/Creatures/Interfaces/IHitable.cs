using UnityEngine;

namespace Creatures.Interfaces
{
    public interface IHitable
    {
        void SetFinalHit(Vector2 finalHit);
        void SetFinalHitDuration(float duration);
    }
}