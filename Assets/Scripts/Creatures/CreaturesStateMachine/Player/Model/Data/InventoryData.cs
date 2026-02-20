using System;
using System.Collections.Generic;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        // список элементов в инвентаре
        [SerializeField] private List<InventoryItemData>  inventoryItems 
            = new List<InventoryItemData>();
        
        // объявление делегата
        // подписываемся в Hero
        public delegate void OnInventoryChanged(string id, int value);
        public OnInventoryChanged OnChanged;

        public void Add(string id, int value)
        {
            if(value <= 0) return;

            // провереям если такой предмет в синглтоне
            var itemDef = DefFacade.Instance.Items.Get(id);
            
            if(itemDef.IsVoid) return;
            
            var item = GetItem(id);
            
            // если предмет есть, то добавляем количество
            if (item != null)
            {
                item.Value += value;
            }
            else
            {
                // если предмета нет, то создадим
                item = new InventoryItemData(id, value);
                inventoryItems.Add(item);
            }
            
            OnChanged?.Invoke(id, value);
            
        }

        // проверка есть ли предмет в инвентаре
        private InventoryItemData GetItem(string id)
        {
            foreach (InventoryItemData item in inventoryItems)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public void Remove(string id, int value)
        {
            // провереям если такой предмет в синглтоне
            var itemDef = DefFacade.Instance.Items.Get(id);
            
            
            if(itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if(item == null) return;
            
            item.Value -= value;

            if (item.Value <= 0)
            {
                inventoryItems.Remove(item);
            }
            
            OnChanged?.Invoke(id, value);
        }

        // метод который вернёт количество определённого предмета
        public int Count(string id)
        {
            foreach (InventoryItemData item in inventoryItems)
            {
                if (item.Id == id)
                    return item.Value;
            }
            
            return 0;
        }
    }

    // предмет
    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}