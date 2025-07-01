using Cinemachine;
using Creatures;
using Creatures.CreaturesStateMachine.Hero;
using UnityEngine;

namespace Components
{
    public class EnterInCameraZone : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera currentCamera;
        public EnterInCameraEvent onEnterInCameraZone;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Hero hero = other.GetComponent<Hero>();
            if (hero != null)
            {
                onEnterInCameraZone?.Invoke(currentCamera);
            }
        }
    }
}