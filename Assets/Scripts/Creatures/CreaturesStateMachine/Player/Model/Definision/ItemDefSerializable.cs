using System;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    public struct ItemDefSerializable
    {
        [SerializeField] private string id;
        public string Id => id;
        public bool IsVoid => string.IsNullOrEmpty(id);
    }
}