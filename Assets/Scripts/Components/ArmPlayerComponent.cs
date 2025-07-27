using Creatures;
using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.Player;
using GameManagerInfo;
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
            if(_gameSession.PlayerData.isArmed) return;
            
            if (!_gameSession.PlayerData.isArmed)
            {
                HeroAnimController anim = hero.GetComponent<HeroAnimController>();
                
                if (anim != null)
                {
                    anim.ChangeArmedState();
                }
            }
        }
    }
}