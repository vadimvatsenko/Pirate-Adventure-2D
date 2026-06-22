using UnityEngine;

namespace Components
{
    public class LoopingClouds : MonoBehaviour
    {
        [SerializeField] private Transform firstCloud;
        [SerializeField] private Transform secondCloud;
        [SerializeField] private float speed = 1f;

        private float _width;

        private void Start()
        {
            _width = firstCloud.GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float move = speed * Time.deltaTime;

            firstCloud.position += Vector3.left * move;
            secondCloud.position += Vector3.left * move;
            
            if (firstCloud.position.x <= -_width)
            {
                firstCloud.position = new Vector3(secondCloud.position.x + _width, firstCloud.position.y, firstCloud.position.z);
            }

            if (secondCloud.position.x <= -_width)
            {
                secondCloud.position = new Vector3(firstCloud.position.x + _width, secondCloud.position.y, secondCloud.position.z);
            }
        }
    }
}