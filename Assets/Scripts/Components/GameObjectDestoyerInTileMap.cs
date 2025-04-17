using PlayerFolder;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Components
{
    public class GameObjectDestoyerInTileMap : MonoBehaviour
    {
        [SerializeField] Tilemap tilemap;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Vector3Int cell = tilemap.WorldToCell(this.transform.position);
                tilemap.SetTile(cell, null);
                
                Destroy(this.gameObject);
            }
        }
    }
}