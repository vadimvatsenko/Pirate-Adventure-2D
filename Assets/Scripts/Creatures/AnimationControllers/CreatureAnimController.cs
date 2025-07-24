using System;
using System.Collections;
using Creatures.CreaturesStateMachine;
using Model;
using UnityEngine;

namespace Creatures
{
    public class CreatureAnimController : MonoBehaviour
    {
        protected Animator CreatureAnim { get; private set; }
        protected Creature Cre {get; private set;}
        
        protected virtual void Awake()
        {
            Cre = GetComponent<Creature>();
            CreatureAnim = GetComponentInChildren<Animator>();
        }
    }
}