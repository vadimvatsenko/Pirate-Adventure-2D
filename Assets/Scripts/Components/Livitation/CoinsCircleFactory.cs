using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.Livitation
{
    public class CoinsCircleFactory : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform coinPrefab;
        [SerializeField] private int count = 8;
        [SerializeField] private float radius = 2f;
        [SerializeField] private float speedDegPerSec = 90f; // скорость вращения в градусах/сек

        private readonly List<Transform> _coins = new List<Transform>();
        private float _angle; // общий угол для всех (синхронно)

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            if (coinPrefab == null || count <= 0) return;

            for (int i = 0; i < count; i++)
            {
                Transform coin = Instantiate(coinPrefab, transform);
                _coins.Add(coin);
            }

            UpdatePositions();
        }

        private void Update()
        {
            if (_coins.Count == 0)
            {
                Dispose();
                return;
            }
            _angle += speedDegPerSec * Time.deltaTime;
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < _coins.Count; i++)
            {
                if (_coins[i] == null)
                {
                    _coins.RemoveAt(i);
                    return;
                }
            }
            
            float step = 360f / count;

            for (int i = 0; i < _coins.Count; i++)
            {
                float a = (_angle + step * i) * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0f) * radius;
                _coins[i].localPosition = offset;
            }
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}