using System.Collections.Generic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine
{
    public abstract class AnimatorHashes
    {
        // static fields
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Move = Animator.StringToHash("move");
        public static readonly int IdleMove = Animator.StringToHash("idleMove");
        public static readonly int JumpFall = Animator.StringToHash("jumpFall");
        public static readonly int YVelocity = Animator.StringToHash("yVelocity");
        public static readonly int XVelocity = Animator.StringToHash("xVelocity");
        public static readonly int Jump = Animator.StringToHash("jump");
        public static readonly int Fall = Animator.StringToHash("fall");
        public static readonly int Attack = Animator.StringToHash("attack");
        public static readonly int Hit = Animator.StringToHash("hit");
        public static readonly int Die = Animator.StringToHash("die");
        public static readonly int Aggro = Animator.StringToHash("aggro");
        public static readonly int Battle = Animator.StringToHash("battle");
        public static readonly int BattleAnimSpeed = Animator.StringToHash("battleAnimSpeed");

        private static readonly Dictionary<int, string> HashToName = new Dictionary<int, string>()
        {
            { Idle, "idle" },
            { Move, "move" },
            { IdleMove, "idleMove" },
            { JumpFall, "jumpFall"},
            { XVelocity, "xVelocity" },
            { YVelocity, "yVelocity" },
            { Jump, "jump" },
            { Attack, "attack" },
            { Hit, "hit" },
            { Die, "die"},
            { Aggro, "aggro" },
            { Battle, "battle" },
            { BattleAnimSpeed, "battleAnimSpeed" },
        };

        public static string GetName(int hash)
        {
            return HashToName[hash];
        }
    }
}