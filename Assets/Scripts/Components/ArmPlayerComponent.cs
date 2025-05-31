using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        public void ArmPlayer()
        {
            var playerAnim = FindObjectOfType<PlayerAnimController>();
            playerAnim.ChangeArmedState();
        }
    }
}