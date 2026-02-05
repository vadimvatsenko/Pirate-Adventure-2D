using System;
using System.Collections.Generic;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class InventoryDataSerializable
    {
        // InventoryItemDataSerializable в себе хранит только id и количество
        [SerializeField] private List<InventoryItemDataSerializable> invertoryItemsData 
            = new List<InventoryItemDataSerializable>();

        public void Add(string id, int value)
        {
            if(value <= 0) return;

            var itemDef = DefsFacadeScriptable.Instance.Items.GetItem(id);
            if(itemDef.IsVoid) return;
            
            var item = GetItem(id);

            if (item != null)
            {
                item.Value = value;
            }
            else
            {
                item = new InventoryItemDataSerializable(id, value);
                invertoryItemsData.Add(item);
            }
        }
        
        public void Remove(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;
            
            var itemDef = DefsFacadeScriptable.Instance.Items.GetItem(id);
            if(itemDef.IsVoid) return;
            
            item.Value -= value;

            if (item.Value <= 0)
            {
                invertoryItemsData.Remove(item);
            }
        }

        private InventoryItemDataSerializable GetItem(string id)
        {
            foreach (var itemData in invertoryItemsData)
            {
                if(itemData.Id == id) return itemData;
            }
            
            return null;
        }
    }
    
}