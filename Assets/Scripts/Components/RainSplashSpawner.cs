using System;
using UnityEngine;

namespace Components
{
    public class RainSplashSpawner :MonoBehaviour
    {
        [SerializeField] private ParticleSystem particlePrefab;

        private void OnParticleCollision(GameObject other)
        {
            Vector3 collisionPos = transform.position;
            ParticleSystem par = Instantiate(particlePrefab, collisionPos, Quaternion.identity);
            
            /*par.transform.SetParent(this.transform);
            
            Destroy(par, 1f);*/
        }
    }
}