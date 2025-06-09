using Creatures;
using Model;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        [SerializeField] private Hero hero;
        private GameSession _gameSession;
        

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }
        public void ArmPlayer()
        {
            
            if (!_gameSession.PlayerData.isArmed)
            {
                Debug.Log("Arming Player");
                //playerAnim.ChangeArmedState();
            }
        }
    }
}