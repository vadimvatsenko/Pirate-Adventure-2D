using UnityEngine;

namespace Components
{
    // угасание спрайта путём подмешивания альфа канала
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFade : MonoBehaviour
    {
        [SerializeField] private float speed;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var currentAlpha = _spriteRenderer.color.a;
            var newAlpha = Mathf.Lerp(currentAlpha, 0, speed * Time.deltaTime);
            _spriteRenderer.color = 
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, currentAlpha);
        }
    }
}

