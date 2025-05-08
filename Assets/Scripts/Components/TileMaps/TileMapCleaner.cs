using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Components.TileMaps
{
    public class TileMapCleaner : MonoBehaviour
    {
        [SerializeField] private string pathToRuleTile;
        private RuleTile[] _ruleTiles;
        private Tilemap _tilemap;

        private readonly Dictionary<string, Sprite> _ruleSprites = new Dictionary<string, Sprite>();

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            _ruleTiles = Resources.LoadAll<RuleTile>(pathToRuleTile);

            foreach (var r in _ruleTiles)
            {
                _ruleSprites.Add(r.name, r.m_DefaultSprite);
                r.m_DefaultSprite = null;
            }
            
            _tilemap.RefreshAllTiles();
        }

        private void OnDisable()
        {
            foreach (var r in _ruleTiles)
            {
                if (_ruleSprites.TryGetValue(r.name, out var originalSprite))
                {
                    r.m_DefaultSprite = originalSprite;
                }
            }
        }
    }
}