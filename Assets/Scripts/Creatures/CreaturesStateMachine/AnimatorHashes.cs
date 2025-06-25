using System.Collections.Generic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public class AnimatorHashes
    {
        // static fields
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Move = Animator.StringToHash("Move");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Die = Animator.StringToHash("Die");

        private static readonly Dictionary<int, string> HashToName = new Dictionary<int, string>()
        {
            { Idle, "Idle" },
            { Move, "Move" },
            { Jump, "Jump" },
            {Attack, "Attack"},
            { Hit, "Hit" },
            {Die, "Die"}
        };

        public static string GetName(int hash)
        {
            return HashToName[hash];
        }

    }
}