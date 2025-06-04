using Cinemachine;
using PlayerFolder;
using UnityEngine;

namespace Components.VirtualCamera
{
    public class TempCameraFollowHero : MonoBehaviour
    {
        private CinemachineVirtualCamera _cam;
        void Start()
        {
            _cam = GetComponent<CinemachineVirtualCamera>();
            var player = FindObjectOfType<Hero>().transform; // временно
            if(player != null) _cam.Follow = player; // временно
        }
    }
}

