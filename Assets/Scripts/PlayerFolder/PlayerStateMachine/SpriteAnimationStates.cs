using UnityEngine;

namespace PlayerFolder
{
    [RequireComponent(typeof(SpriteRenderer))] 
    public class SpriteAnimationStates : MonoBehaviour
    {
        private AnimationClip _animationClip;
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
        }

        private void Update()
        {
            // Если ещё не пришло время смены кадра (_nextFrameTime > Time.time), тоже выходим.
            if (!_isPlaying || _nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _animationClip.Sprites.Count)
            {
                if (_animationClip.Loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _isPlaying = false;
                    _animationClip.OnComplete?.Invoke();
                    return;
                }
            }

            _spriteRenderer.sprite = _animationClip.Sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            _currentSpriteIndex++;

        }

        public void SetAnimationClip(AnimationClip animationClip)
        {
            _isPlaying = true;
            _animationClip = animationClip;
            _frameTime = _animationClip.FrameRate;
        }
    }
}
