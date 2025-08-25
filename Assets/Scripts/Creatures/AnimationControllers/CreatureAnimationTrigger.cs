using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

// вешается на аниматор
// можно через инспектор повесить событие на AttackTrigger()
namespace Creatures.AnimationControllers
{
    public class CreatureAnimationTrigger : MonoBehaviour
    {
        private Creature _creature;
        private CombatCollisions _combatCollisions;

        private void Awake()
        {
            _creature = GetComponentInParent<Creature>();
            _combatCollisions = GetComponentInParent<CombatCollisions>();
        }

        private void AttackTrigger()
        {
            //Debug.Log("Attack Trigger");
            _combatCollisions.PerformAttack();
        }
    }
}