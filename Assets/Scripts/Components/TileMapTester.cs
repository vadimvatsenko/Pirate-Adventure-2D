using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Components
{
    public class TileMapTester : MonoBehaviour
    {
        private Tilemap _tilemap;
        BoundsInt _bounds;
        private int _count = 0;
        
        List<Vector3Int> _list = new List<Vector3Int>();
        
        
        private void Start()
        {
            _tilemap = GetComponent<Tilemap>();
            
            _bounds = _tilemap.cellBounds;
            
        }

        private void Update()
        {
            List<Vector3Int> toRemove = new List<Vector3Int>();
            for (int x = _bounds.min.x; x < _bounds.max.x; x++)
            {
                for (int y = _bounds.min.y; y < _bounds.max.y; y++)
                {
                    for (int z = _bounds.min.z; z < _bounds.max.z; z++)
                    {
                        Vector3Int pos = new Vector3Int(x, y, z);

                        if (_tilemap.HasTile(pos))
                        {
                            // Если позиция ещё нет в списке, добавляем её
                            if (!_list.Contains(pos))
                            {
                                _list.Add(pos);
                            }
                        }
                        else
                        {
                            // Если на позиции больше нет плитки, планируем её удалить из списка
                            if (_list.Contains(pos))
                            {
                                toRemove.Add(pos);
                            }
                        }

                    }
                }

                
            }
            foreach (var pos in toRemove)
            {
                _list.Remove(pos);
            }

            //Debug.Log(_list.Count);
        }
        
    }
}