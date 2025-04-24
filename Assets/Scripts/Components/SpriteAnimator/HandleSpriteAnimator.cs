using Components.SpriteAnimator;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerFolder
{
    [RequireComponent(typeof(SpriteRenderer))] 
    public class HandleSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private bool isStartWithRandomSprite = false;
        [SerializeField] private bool isReversed = false;
        
        [SerializeField] private UnityEvent onComplete;
        
        private HandleAnimationClip _animationClip;
        private SpriteRenderer _spriteRenderer;

        private int _frameTime;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;

        private bool _isPlaying = false;
        
        private void Start()
        {
            _frameTime = _animationClip.FrameRate;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _secondPerFrame = 1.0f / _frameTime;
            _nextFrameTime = Time.time + _secondPerFrame;
            
            int randomSpriteIndex = UnityEngine.Random.Range(0, _animationClip.Sprites.Count);

            if (isStartWithRandomSprite)
            {
                _currentSpriteIndex = randomSpriteIndex;
            }
            else
            {
                if (isReversed)
                {
                    _currentSpriteIndex = _animationClip.Sprites.Count - 1;
                }
                else
                {
                    _currentSpriteIndex = 0;
                }
            }
        }
        
        // добавили два метода, OnBecameVisible и OnBecameInvisible
        // чтобы анимация включалась только тогда, когда видно компонент
        private void OnBecameVisible() => enabled = _isPlaying;
        private void OnBecameInvisible() => enabled = false;
        
        private void Update()
        {
            // Если ещё не пришло время смены кадра (_nextFrameTime > Time.time), тоже выходим.
            if (!_isPlaying || _nextFrameTime > Time.time) return;
            
            bool isOutOfForwardBounds = _currentSpriteIndex >= _animationClip.Sprites.Count;
            bool isOutOfReverseBounds = _currentSpriteIndex < 0;
            
            if (isOutOfForwardBounds || isOutOfReverseBounds)
            {
                if (_animationClip.Loop)
                {
                    _currentSpriteIndex = isReversed ? _animationClip.Sprites.Count - 1 : 0;
                }
                else
                {
                    _isPlaying = false;
                    onComplete?.Invoke();
                    return;
                }
            }
            
            _spriteRenderer.sprite = _animationClip.Sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            
            _currentSpriteIndex = isReversed ? _currentSpriteIndex - 1 : _currentSpriteIndex + 1;
        }

        public void SetAnimationClip(HandleAnimationClip animationClip)
        {
            _animationClip = animationClip;
            _frameTime = animationClip.FrameRate;
            
            _secondPerFrame = 1f / _frameTime;
            _nextFrameTime = Time.time + _secondPerFrame;
        }

        public void PlayAnimation()
        {
            _isPlaying = true;
            enabled = true; // активация скрипта
        }
    }
}
