using System;
using System.Collections.Generic;
using UnityEngine;

namespace Creatures.CreaturesStateMachine.Player.Model.Definision
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItemDef")]
    // описание предмета
    public class InventoryItemDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] items;

        public ItemDef Get(string id)
        {
            foreach (var i in items)
            {
                if (i.Id == id)
                    return i;
            }

            // структура не может быть null, потому default
            return default;
        }
    }

    // структура
    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string id;
        public string Id => id;
        
        // проверка на пустоту структуры
        public bool IsVoid => string.IsNullOrEmpty(id);
    }
}