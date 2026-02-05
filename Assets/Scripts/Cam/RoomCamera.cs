using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Cam
{
    public class RoomCamera : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Tilemap tilemap;
        
        
        private CinemachineVirtualCamera _virtualCam;
        private HashSet<Vector3Int> _tilePositions;

        private Transform _followingTarget;
        private Vector3Int _lastPlayerCell;
        
        private void Awake()
        {
            _virtualCam = GetComponent<CinemachineVirtualCamera>();
            _tilePositions = new HashSet<Vector3Int>();
        }
        void Start()
        {
            
            BoundsInt bounds = tilemap.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y= bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    if (tilemap.HasTile((Vector3Int)position))
                    {
                        _tilePositions.Add(position);
                    }
                }
            }
            
            _followingTarget = new GameObject("FollowingTarget").transform;
            _virtualCam.Follow = _followingTarget;
        }

        private void Update()
        {
            Vector3Int playerCell = tilemap.WorldToCell(player.position);

            if (playerCell == _lastPlayerCell) return; // игрок не переместился в новую клетку

            _lastPlayerCell = playerCell;

            if (_tilePositions.Contains(playerCell))
            {
                Vector3 worldPos = tilemap.GetCellCenterWorld(playerCell);
                _followingTarget.position = worldPos;
            }
        }
    }
}