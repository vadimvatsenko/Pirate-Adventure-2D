using System;
using Components.SpriteAnimator;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerFolder
{
    [RequireComponent(typeof(SpriteRenderer))] 
    public class HandleSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private UnityEvent onComplete;
        [SerializeField] private bool isStartWithRandomSprite;
        [SerializeField] private bool isReversed = false;
        
        private HandleAnimationClip _animationClip;
        private SpriteRenderer _spriteRenderer;

        private int _frameTime;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;

        private bool _isPlaying = true;
        
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
        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void Update()
        {
            
            // Если ещё не пришло время смены кадра (_nextFrameTime > Time.time), тоже выходим.
            if (!_isPlaying || _nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _animationClip.Sprites.Count)
            {
                if (_animationClip.Loop)
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
                else
                {
                    _isPlaying = false;
                    onComplete?.Invoke();
                    return;
                }
            }
            
            _spriteRenderer.sprite = _animationClip.Sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            
            if (isReversed)
            {
                _currentSpriteIndex--;
            }
            else
            {
                _currentSpriteIndex++;
            }
        }

        public void SetAnimationClip(HandleAnimationClip animationClip)
        {
            _isPlaying = true;
            _animationClip = animationClip;
            _frameTime = animationClip.FrameRate;
        }
    }
}
