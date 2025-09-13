using UnityEngine;

namespace Items.Elevator
{
    public class ChainForElevator : MonoBehaviour
    {
        [SerializeField] private float targetHeight = 20f;
        [SerializeField] private float targetDuration = 5f;
        
        private float _time = 0f;
        private SpriteRenderer _spriteRenderer;
        private Vector3 _starSize;
        

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _starSize = _spriteRenderer.size;
        }

        private void Update()
        {
            if (_time < targetDuration)
            {
                
                _time += Time.deltaTime;
                float percentage = _time / targetDuration;
                _spriteRenderer.size = Vector2.Lerp(_starSize, new Vector2(_starSize.x, targetHeight), percentage);
            }
        }
    }
}