using UnityEngine;

namespace PlayerFolder
{
    public abstract class Player : MonoBehaviour
    {
        public abstract void SetDirection(float direction);
        public abstract void HandleJump(bool isPressed);
    }
}