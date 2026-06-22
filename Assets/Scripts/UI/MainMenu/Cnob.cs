using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class Cnob : MonoBehaviour
    {
        [SerializeField] private float duration = 2;
        [SerializeField] private Vector3 startScale =  Vector3.zero;
        [SerializeField] private Vector3 endScale  = new Vector3(7,7,7);
        [SerializeField] private Color startColor = Color.black;
        [SerializeField] private Color endColor = Color.black;
        
        private RectTransform _rectTransform;
        private RawImage _image;
        
        private float _time = 0f;
        
        public Action onFinish;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<RawImage>();
        }
        
        public void AnimateRound() => StartCoroutine(AnimateRoundRoutine());

        private IEnumerator AnimateRoundRoutine()
        {
            _time = 0f;
            
            while (_time < duration)
            {
                _time += Time.deltaTime;
                float t = _time / duration;

                _rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
                _image.color = Color.Lerp(startColor, endColor, t);

                yield return null;
            }

            _rectTransform.localScale = endScale;
            _image.color = endColor;
            
            onFinish?.Invoke();
        }
        
    }
}