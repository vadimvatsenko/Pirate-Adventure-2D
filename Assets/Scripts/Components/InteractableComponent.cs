using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;

        public void Interact()
        {
            Debug.Log("Interact");
            action?.Invoke();
        }
    }
}