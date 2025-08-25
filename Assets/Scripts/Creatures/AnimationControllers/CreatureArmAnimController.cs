using System;
using System.Collections;
using Creatures.CreaturesStateMachine;
using Creatures.CreaturesStateMachine.CreatureBasic;
using UnityEngine;

namespace Creatures
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