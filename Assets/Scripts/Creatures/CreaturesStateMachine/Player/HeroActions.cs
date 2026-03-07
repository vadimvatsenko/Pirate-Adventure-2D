using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.CreaturesStateMachine.Player
{
    public class HeroActions : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnJump;
        [SerializeField] private UnityEvent OnDoubleJump;
        [SerializeField] private UnityEvent OnHit;
        
        
    }
}