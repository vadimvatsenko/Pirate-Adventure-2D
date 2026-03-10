using Creatures.CreaturesCollisions;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostDeathState : GhostBaseState
    {
        private float _deathRotationDirection = 1;
        private float _deathRotationSpeed = 200;
        
        public GhostDeathState(Ghost creature, BasicStateMachine stateMachine, int animBoolName) : base(creature, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Ghost.GetComponent<CombatCollisions>().enabled = false;
            
            if (Random.Range(0f, 100f) < 50) // ++
            {
                _deathRotationDirection *= -1;
            }
        }

        public override void Update()
        {
            base.Update();
            
            HandleDeathRotation();
        }
        
        private void HandleDeathRotation()
        {
            Ghost.transform.position += Vector3.down * (5 * Time.deltaTime);
            Ghost.transform.Rotate(0,0,(_deathRotationDirection * _deathRotationSpeed) * Time.deltaTime); // ++
        }
    }
}