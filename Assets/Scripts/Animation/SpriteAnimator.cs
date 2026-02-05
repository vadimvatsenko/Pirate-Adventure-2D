using UnityEngine;
using UnityEngine.Events;

// Анимационная система с уроков
namespace Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [Range(1, 30)] private readonly int frameRate = 10;
        [SerializeField] private UnityEvent<string> onComplete;
        [SerializeField] private AnimationClip[] clips;

        private SpriteRenderer _renderer;

        private float _secPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private bool _isPlaying = true;

        private int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secPerFrame = 1f / frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (var i = 0; i < clips.Length; i++)
            {
                if (clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaying = false;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            var clip = clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, clips.Length);
                    }
                }

                return;
            }

            _renderer.sprite = clip.Sprites[_currentFrame];

            _nextFrameTime += _secPerFrame;
            _currentFrame++;
        }
    }
}