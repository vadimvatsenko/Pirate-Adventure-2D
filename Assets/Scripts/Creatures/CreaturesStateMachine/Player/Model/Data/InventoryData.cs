using System;
using System.Collections.Generic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> invertoryItemData = null;
    }
    
}