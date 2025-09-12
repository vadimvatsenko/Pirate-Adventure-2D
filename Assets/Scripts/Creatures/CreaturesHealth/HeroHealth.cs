using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Creatures.CreaturesHealth
{
    public class HeroHealth : CreatureHealth
    {
        private Hero _hero;
        protected override void Awake()
        {
            base.Awake();
            _hero = gameObject.GetComponent<Hero>();
        }

        public override void TakeDamage(float damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
        }
    }
}