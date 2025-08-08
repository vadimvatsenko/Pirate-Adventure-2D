using UnityEngine;
using Cinemachine;
using Creatures.CreaturesStateMachine.Player;

namespace Cam
{
    public class RoomConfiner : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private CinemachineConfiner _confiner;

        private void Awake()
        {
            _confiner = virtualCamera.GetComponent<CinemachineConfiner>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Hero hero = other.GetComponent<Hero>();

            if (hero != null)
            {
                Debug.Log("new shape");
                _confiner.m_BoundingShape2D = this.GetComponent<Collider2D>();
                _confiner.InvalidatePathCache();
                virtualCamera.Follow = this.transform;
            }
        }
    }
    
}