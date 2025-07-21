using UnityEngine;

namespace Utils
{
    public struct Timer
    {
        private float _timeLeft;
        private float _duration;
        private bool _isRunning;
        
        public bool IsRunning => _isRunning;
        public float TimeLeft => _timeLeft;

        public Timer(float duration)
        {
            _duration = duration;
            _timeLeft = 0;
            _isRunning = false;
        }

        public void Start()
        {
            _timeLeft = _duration;
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public bool Update()
        {
            if (!_isRunning) return false;

            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0f)
            {
                _isRunning = false;
                return true; // Таймер истёк
            }

            return false; // Ещё тикает
        }

    }
}
