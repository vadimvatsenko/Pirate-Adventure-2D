using System.Collections;
using UnityEngine;

// мигание персонажа при уроне
namespace Creatures.CreaturesVFX
{
    public class CreatureVFX : MonoBehaviour
    {
        [SerializeField] private Material onDamageMaterial;
        [SerializeField] private float onDamageVFXDuration;
        
        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private Coroutine _onDamageVFXCoroutine;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;

            if (_onDamageVFXCoroutine != null) 
                StopCoroutine(_onDamageVFXCoroutine);
            else
                _onDamageVFXCoroutine = StartCoroutine(OnDamageVFX());
        }

        public void PlayOnDamageVFX() => StartCoroutine(OnDamageVFX());
        private IEnumerator OnDamageVFX()
        {
            _spriteRenderer.material = onDamageMaterial;
            yield return new WaitForSeconds(onDamageVFXDuration);
            _spriteRenderer.material = _originalMaterial;
        }
    }
}