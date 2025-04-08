using UnityEngine;

namespace Components
{
    public class SpriteAlphaPulse : MonoBehaviour
    {
        [SerializeField] private float minAlpha = 0.2f;
        [SerializeField] private float maxAlpha = 1.0f;
        [SerializeField] private float pulseSpeed = 2.0f;
        
        private SpriteRenderer _spriteRenderer;
        private float _alphaTimer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _alphaTimer = Random.Range(0, Mathf.PI * 2);
        }

        private void FixedUpdate()
        {
            _alphaTimer += Time.fixedDeltaTime * pulseSpeed;

            // Альфа по синусоиде (0–1 → min–max)
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(_alphaTimer) + 1f) / 2f);

            Color color = _spriteRenderer.color;
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}