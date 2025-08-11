using System.Diagnostics;
using UnityEngine;
using Cinemachine;
using Creatures.CreaturesStateMachine.Player;

namespace Cam
{
    public class RoomController : MonoBehaviour
    {
        private CameraBoundsSwitcher _cameraBoundSwitcher;
        private void Awake()
        {
            _cameraBoundSwitcher = FindObjectOfType<CameraBoundsSwitcher>();
        }

        public void SwitchRoom()
        {
            _cameraBoundSwitcher.MoveTo(this.transform.position);
        }
    }
    
}