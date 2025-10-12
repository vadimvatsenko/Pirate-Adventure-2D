using Creatures.CreaturesStateMachine.Player;
using UnityEngine;

namespace Components.Parallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxFactor; // чем меньше значение, тем медленнее слой двигается
        private Transform _cameraTransform;
        private Vector3 _previousCameraPosition;
        
        private bool _isParallaxActive;
        private Hero _hero;

        void Start()
        {
            _cameraTransform = Camera.main.transform;
            _previousCameraPosition = _cameraTransform.position;
            
            _hero = FindObjectOfType<Hero>();
            
            _cameraTransform = _hero.transform;
            _previousCameraPosition = _cameraTransform.position;
        }

        void LateUpdate()
        {
            if (_isParallaxActive)
            {
                Vector3 delta = _cameraTransform.position - _previousCameraPosition;
                //transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
                transform.position -= new Vector3(delta.x * parallaxFactor.x, delta.y * parallaxFactor.y, 0);
                _previousCameraPosition = _cameraTransform.position;
            }
        }
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();
            if (hero != null)
            {
                _isParallaxActive = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Hero hero = collision.GetComponent<Hero>();

            if (hero != null)
            {
                _isParallaxActive = false;
            }
        }
    }
}