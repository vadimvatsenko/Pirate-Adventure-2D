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
        private CreatureCombatCollisions _creatureCombatCollisions;

        private void Awake()
        {
            _creature = GetComponentInParent<Creature>();
            _creatureCombatCollisions = GetComponentInParent<CreatureCombatCollisions>();
        }

        private void AttackTrigger()
        {
            Debug.Log("Attack Trigger");
            _creatureCombatCollisions.PerformAttack();
        }
    }
}