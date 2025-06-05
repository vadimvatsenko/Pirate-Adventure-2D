using Creatures;
using Model;
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
            var playerAnim = FindObjectOfType<CratureAnimController>();
            
            if (!_gameSession.PlayerData.isArmed)
            {
                Debug.Log("Arming Player");
                playerAnim.ChangeArmedState();
            }
        }
    }
}