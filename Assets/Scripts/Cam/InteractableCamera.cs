using UnityEngine;

namespace Cam
{
    public class InteractableCamera : MonoBehaviour
    {
        //[SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Camera.main.transform.position = this.transform.position;
            }
        }
    }
}