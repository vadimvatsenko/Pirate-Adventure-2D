using UnityEngine;

namespace Components.Parallax
{
    public class ParallaxClouds : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        [SerializeField] private float _speed = 2f;

        private void Awake()
        {
            _speed = 0.1f;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void LateUpdate()
        {
            _meshRenderer.material.mainTextureOffset += new Vector2(_speed, 0);
        }
    }
}