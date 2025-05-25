using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Tile Palette/Items
namespace Components.TileMaps
{
    public class TileMapCleaner : MonoBehaviour
    {
        [SerializeField] private string[] pathToRuleTiles;
        private RuleTile[] _ruleTiles;
        private Tilemap _tilemap;

        private readonly Dictionary<string, Sprite> _ruleSprites = new Dictionary<string, Sprite>();

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();

            List<RuleTile> loadedTiles = new List<RuleTile>();

            foreach (string path in pathToRuleTiles)
            {
                var tilesFromPath = Resources.LoadAll<RuleTile>(path);
                loadedTiles.AddRange(tilesFromPath);
            }
            
            _ruleTiles = loadedTiles.ToArray();

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