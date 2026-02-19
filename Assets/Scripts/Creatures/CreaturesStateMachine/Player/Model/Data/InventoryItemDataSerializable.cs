using System;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class InventoryItemDataSerializable
    {
        public string Id;
        public int Value;

        public InventoryItemDataSerializable(string id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}