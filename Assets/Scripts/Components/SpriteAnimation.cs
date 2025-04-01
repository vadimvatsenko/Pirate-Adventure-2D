using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    [RequireComponent(typeof(SpriteRenderer))] //  появится компонент в любом случае
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int frameRate;
        [SerializeField] private bool loop;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private UnityEvent onComplete;

        private SpriteRenderer _spriteRenderer;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;

        private bool _isPlaying = true;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _secondPerFrame = 1.0f / frameRate;
            _nextFrameTime = Time.time + _secondPerFrame;
        }

        private void Update()
        {
            if (!_isPlaying || _nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= sprites.Length)
            {
                if (loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _isPlaying = false;
                    onComplete?.Invoke();
                    return;
                }
            }

            _spriteRenderer.sprite = sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            _currentSpriteIndex++;

        }
    }
}
