using System;
using System.Collections.Generic;
using Creatures.CreaturesStateMachine.Player.Model.Definision;
using Creatures.CreaturesStateMachine.Player.Model.Definision.EditorHelper;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        // список элементов в инвентаре
        [SerializeField] private List<InventoryItemData>  inventoryItems 
            = new List<InventoryItemData>();

        public List<InventoryItemData> InventoryItem => inventoryItems;
        
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
            
            // проверка на переполнение инвентаря
            bool isFull = inventoryItems.Count >= DefFacade.Instance.PlayerDef.InventorySize;

            // если предмет стаскаемый 
            if (itemDef.IsStackable)
            {
                var item = GetItem(id);
            
                // если предмет есть, то добавляем количество
                if (item == null)
                {
                    // если инвентарь полный, то выходим
                    if (isFull)
                    {
                        Debug.LogError($"Inventory Full");
                    }
                    // если предмета нет, то создадим
                    item = new InventoryItemData(id, value);
                    inventoryItems.Add(item);
                   
                }
                item.Value += value;
            }

            else
            {
                for (int i = 0; i < value; i++)
                {
                    isFull = inventoryItems.Count >= DefFacade.Instance.PlayerDef.InventorySize;
                    if (isFull)
                    {
                        return;
                    }
                    
                    var item = new InventoryItemData(id, value) {Value = 1};
                    inventoryItems.Add(item);
                }
            }
            
            OnChanged?.Invoke(id, value);
        }
        
        public void Remove(string id, int value)
        {
            // провереям если такой предмет в синглтоне
            var itemDef = DefFacade.Instance.Items.Get(id);
            
            
            if(itemDef.IsVoid) return;

            if (itemDef.IsStackable)
            {
                var item = GetItem(id);
                if(item == null) return;
            
                item.Value -= value;

                if (item.Value <= 0)
                {
                    inventoryItems.Remove(item);
                }
            }

            else
            {
                for (int i = 0; i < value; i++)
                {
                    var item = GetItem(id);
                    if(item == null) return;
                    
                    inventoryItems.Remove(item);
                }
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