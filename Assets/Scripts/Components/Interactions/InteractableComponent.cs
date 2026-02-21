using UnityEngine;
using UnityEngine.Events;

namespace Components.Interactions
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        [SerializeField] private bool isDisposable;

        private bool _used;
        private bool _isActive = true;
        public void Interact()
        {
            Debug.Log("Interacting..." +  this.name);
            if (_used && !_isActive) return;
            
            action?.Invoke();
            
            if (!isDisposable)
            {
                _used = true;
                _isActive = false;
            }
        }
        public void Activate() => _isActive = true;
        
    }
}