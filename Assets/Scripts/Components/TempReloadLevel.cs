using System;
using GameManagerInfo;
using UnityEngine;

namespace Components
{
    public class TempReloadLevel : MonoBehaviour
    {
        public GameSession GameSession { get; private set; }

        public void Awake()
        {
            GameSession = FindObjectOfType<GameSession>();
        }

        public void ReloadLevel()
        {
            GameSession.ReloadLevel();
        }
    }
}