using UnityEngine;

namespace Components.Levitation
{
    public class BaseLevitationComponent : MonoBehaviour
    {
        [SerializeField] protected float frequency = 1;
        [SerializeField] protected float amplitude = 1;
        [SerializeField] protected bool randomize;
        
        protected float OriginalY;
        protected Rigidbody2D Rigidbody2D;
        protected float Seed;

        protected virtual void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            OriginalY = Rigidbody2D.position.y;
            if (randomize)
            {
                Seed = Random.value * Mathf.PI * 2; // пи * 2 это круг в радианах
            }
        }
    }
}