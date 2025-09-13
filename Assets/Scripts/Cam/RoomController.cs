using UnityEngine;

namespace Cam
{
    public class RoomController : MonoBehaviour
    {
        private CameraBoundsSwitcher _cameraBoundSwitcher;
        private void OnEnable()
        {
            _cameraBoundSwitcher = FindObjectOfType<CameraBoundsSwitcher>();
        }

        public void SwitchRoom() => _cameraBoundSwitcher.MoveTo(this.transform.position);
    }
}