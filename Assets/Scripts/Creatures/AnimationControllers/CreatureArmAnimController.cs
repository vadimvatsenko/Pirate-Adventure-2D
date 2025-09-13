using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures.AnimationControllers
{
    public class CreatureArmAnimController : MonoBehaviour
    {
        protected Animator CreatureAnim { get; private set; }
        protected Creature Cre {get; private set;}
        
        protected virtual void Awake()
        {
            Cre = GetComponentInParent<Creature>();
            CreatureAnim = GetComponent<Animator>();
        }
    }
}