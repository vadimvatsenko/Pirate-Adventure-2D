using System.Linq;
using Components;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.SharkyEnemy
{
    public class SharkyAttackState : SharkyBattleState
    {
        public SharkyAttackState(Sharky enemySharky, CreatureStateMachine stateMachine, int animBoolName) 
            : base(enemySharky, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            EnemySharky.Rb2D.velocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            
            GameObject[] goArray = EnemySharky.SharkyCollisionInfo.GetObjectsInRange();
            
            HealthComponent[] healthComponents 
                = goArray.Select(obj => obj.GetComponent<HealthComponent>()).ToArray();

            Debug.Log(healthComponents[0].name);
            
            if (!EnemySharky.SharkyCollisionInfo.HeroDetection())
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyMoveState);
            }

            else if(!WithinAttackRange())
            {
                EnemySharky.StateMachine.ChangeState(EnemySharky.SharkyBattleState);
            }
        }
    }
}