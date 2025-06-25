using System;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyStatesController : ICreatureStatesController
    {
        private readonly Creature _creature;
        private readonly CreatureCollisionInfo _collisionInfo;
        private readonly CreatureStateMachine _creaturesStateMachine;

        private float _idleTimer = 0f;
        private float _idleDuration = 3f;

        public SharkyStatesController(Creature creature, CreatureStateMachine stateMachine,
            CreatureCollisionInfo collisionInfo)
        {
            _creature = creature;
            _creaturesStateMachine = stateMachine;
            _collisionInfo = collisionInfo;
        }

        public void Update()
        {
            _idleTimer += Time.deltaTime;

            if (_idleTimer >= _idleDuration && !_collisionInfo.IsAbyssDetected)
            {
                _creaturesStateMachine.ChangeState(_creature.MoveState);
            }

            if (_collisionInfo.IsAbyssDetected)
            {
                _creaturesStateMachine.ChangeState(_creature.IdleState);
            }
            
        }

        private void StatesSwitcher()
        {
            /*if (_collisionInfo.IsAbyssDetected)
            {
                _creaturesStateMachine.ChangeState(_creature.IdleState);
                _creature.SetDirection(0);
                _creature.HandleFlip();
                _idleTimer = 3f;
            }*/
        }
    }
}
