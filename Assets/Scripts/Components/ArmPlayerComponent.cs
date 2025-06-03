using DefaultNamespace.Model;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        private GameSession _gameSession;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        public void ArmPlayer()
        {
            var playerAnim = FindObjectOfType<PlayerAnimController>();
            
            if(!_gameSession.PlayerData.isArmed)
            playerAnim.ChangeArmedState();
        }
    }
}