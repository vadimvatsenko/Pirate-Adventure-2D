using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.Player;
using GameManagerInfo;
using UnityEngine;

namespace Components
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        private Hero _hero;
        private GameSession _gameSession;
        
        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _hero = FindObjectOfType<Hero>();
        }
        public void ArmPlayer()
        {
            if(_gameSession.PlayerData.isArmed) return;
            
            if (!_gameSession.PlayerData.isArmed)
            {
                HeroArmAnimController armAnim = _hero.GetComponentInChildren<HeroArmAnimController>();
                
                if (armAnim != null)
                {
                    armAnim.ChangeArmedState();
                }
            }
        }
    }
}