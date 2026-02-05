using UnityEngine;
using UnityEngine.Serialization;

namespace Cam
{
    public class InteractableCamera : MonoBehaviour
    {
        
        [SerializeField] private Transform player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Camera.main.transform.position = this.transform.position;
            }
        }
    }
}