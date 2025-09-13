using UnityEngine;

namespace Items.GatesSwitchers
{
    public class GatesLever : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public void ActivateOrDeactivate()
        {
            IsActive = !IsActive;
        }
    }
}