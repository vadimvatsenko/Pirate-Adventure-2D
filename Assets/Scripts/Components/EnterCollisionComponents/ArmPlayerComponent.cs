using Creatures.AnimationControllers;
using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Components.EnterCollisionComponents
{
    public class ArmPlayerComponent : MonoBehaviour
    {
        public void ArmCreature(GameObject go)
        {
            var hero = go.gameObject.GetComponent<Hero>();
            
            if(hero.GameSess.PlayerData.isArmed) return;
            
            var animator = go.GetComponentInChildren<HeroArmAnimController>();
            
            if (animator != null)
            {
                animator.ChangeArmedState();
            }
        }
    }
}