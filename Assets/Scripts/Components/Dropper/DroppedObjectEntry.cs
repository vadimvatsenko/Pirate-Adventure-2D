using System;
using UnityEngine;

namespace Components.Dropper
{
    [Serializable]
    public class DroppedObjectEntry
    {
        public GameObject prefab;
        public int amount;
    }
}