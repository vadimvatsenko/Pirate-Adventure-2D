using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Components.EnterCollisionComponents
{
    public class EnterTriggerLayerMask : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private UnityEvent onEnter;

        private List<int> _layerMasksIndex = new List<int>();

        private void Start()
        {
            _layerMasksIndex = LayerUtils.GetLayerIndices(layerMask);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherLayer = other.gameObject.layer;

            if (_layerMasksIndex.Count != 0 && _layerMasksIndex.Contains(otherLayer))
            {
                onEnter?.Invoke();
            }
        }
    }
}