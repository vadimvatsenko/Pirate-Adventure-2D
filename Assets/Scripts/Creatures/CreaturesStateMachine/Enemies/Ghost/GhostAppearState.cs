using Animation;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Enemies.Ghost
{
    public class GhostAppearState : GhostBaseState
    {
        private float xOffSet;
        private float yPosition;
        
        public GhostAppearState(Ghost ghost, BasicStateMachine stateMachine, int animBoolName) : base(ghost, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            xOffSet = Random.Range(0, 100) < 50 ? -2 : 2;
            yPosition = Random.Range(Ghost.YMinDistance, Ghost.YMaxDistance);
            
            Ghost.transform.position = Ghost.GetHero().transform.position + new Vector3(xOffSet, yPosition, 0);
            MakeVisible();
        }

        public override void Update()
        {
            base.Update();
            
            if(StateInfo.IsName(AnimatorHashes.GetName(AnimatorHashes.Appear)) && StateInfo.normalizedTime > 1.0f)
            {
                Debug.Log("Ghost chase");
                StateMachine.ChangeState(Ghost.ChaseState);
            }
        }
        
        private void MakeVisible()
        {
            Ghost.SpriteRenderer.color = Color.white;
            Ghost.C2D.enabled = true;
        }
    }
}