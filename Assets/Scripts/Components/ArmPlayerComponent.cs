using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        public void ArmPlayer(GameObject go)
        {
            var playerAnim = go.GetComponent<PlayerAnimController>();
            playerAnim.ChangeArmedState();
        }
    }
}