using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Cam
{
    public class CameraBoundsSwitcher: MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera vcam;
        [SerializeField] private float delay = 3f;
        
        private float time = 0f;
        private CinemachineConfiner _confiner;
        
        private GameObject _prevCollider;
        
        private void Start()
        {
            _confiner = vcam.GetComponent<CinemachineConfiner>();
            time = 0f;
        }
        
        public void SwitchCameraBounds(GameObject other)
        {
            if (_prevCollider != other)
            {
                while (time < delay)
                {
                    time += Time.deltaTime;
                    
                    Vector2 direction = (other.transform.position - _prevCollider.transform.position).normalized;
                    
                    var targetPos = Vector2.Lerp(_prevCollider.transform.position, other.transform.position, time / delay);
                    
                    
                    vcam.Follow = other.transform;
                    
                    
                }
                _confiner.m_BoundingShape2D = other.GetComponent<Collider2D>();
                time = 0f;
            }
            
            //vcam.Follow = other.transform;
        }
        
    }
}