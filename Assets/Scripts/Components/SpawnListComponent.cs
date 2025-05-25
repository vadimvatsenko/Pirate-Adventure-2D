using System;
using System.Linq;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class SpawnData
    {
        public string Id;
        public SpawnComponent Component;
    }
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void Spawn(string id)
        {
            var spawner = _spawners.FirstOrDefault(el => el.Id == id);
            spawner?.Component.Spawn();
        }

        public void SpawnAll()
        {
            foreach (var spawnData in _spawners)
            {
                spawnData?.Component.Spawn();
            }
        }
    }
}