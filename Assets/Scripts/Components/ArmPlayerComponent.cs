using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        public void ArmPlayer(GameObject go)
        {
            var player = go.GetComponent<Player>();
            player.ChangeArmedState();
        }
    }
}