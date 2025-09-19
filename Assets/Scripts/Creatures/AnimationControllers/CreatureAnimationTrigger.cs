using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

// вешается на аниматор
// можно через инспектор повесить событие на AttackTrigger() итп.
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
            _combatCollisions.PerformAttack();
            _creature.CallOnAttackEvent();
        }

        // триггер броска оружия, вызывается в аниматоре
        private void ThrowTrigger()
        {
            _creature.CallOnThrowEvent();
        }
    }
}