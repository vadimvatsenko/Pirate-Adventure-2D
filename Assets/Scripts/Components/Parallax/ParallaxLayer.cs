using UnityEngine;

namespace Components.Parallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxFactor; // чем меньше значение, тем медленнее слой двигается
        private Transform _cameraTransform;
        private Vector3 _previousCameraPosition;

        void Start()
        {
            _cameraTransform = Camera.main.transform;
            _previousCameraPosition = _cameraTransform.position;
        }

        void LateUpdate()
        {
            Vector3 delta = _cameraTransform.position - _previousCameraPosition;
            //transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
            transform.position += new Vector3(delta.x * parallaxFactor.x, delta.y * parallaxFactor.y, 0);
            _previousCameraPosition = _cameraTransform.position;
        }
    }
}