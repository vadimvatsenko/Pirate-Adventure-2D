using System;
using System.Collections;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    public class FloodWater : MonoBehaviour
    {
        [SerializeField] private Transform targetPos;
        [SerializeField] private float delay = 2f;
        private Vector3 worldTransform;

        private void Awake()
        {
            worldTransform = targetPos.position;
        }
        
        public void Move() => StartCoroutine(MoveCoroutine());

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSecondsRealtime(delay);
            float duration = 100f;
            float elapsedTime = 0f;

            while (duration > elapsedTime)
            {
                transform.position = Vector3.Lerp(transform.position, worldTransform, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = worldTransform;
        }
    }
}