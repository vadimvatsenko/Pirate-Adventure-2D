using System.Collections;
using NaughtyAttributes;
using UnityEngine;

namespace Components
{
    public class GameObjectDropper : MonoBehaviour
    {
        // используется для выброса объектов
        [SerializeField] private GameObject[] prefabs;
        
        // using NaughtyAttributes - теперь можно прятать поля по условию
        // в данном случае если только один префаб, то это поле будет видно
        [ShowIf("HasOnePrefab")] 
        [Range(1, 10)]
        [SerializeField] private int gameObjectCountToDrop = 10;
        
        [Range(0.1f, 10f)]
        [SerializeField] private float spreadForce = 1.5f;
        [Range(0.1f, 10f)]
        [SerializeField] private float spreadRadius = 1.5f;
        [SerializeField] private DropperDirection dropperDirection = DropperDirection.Right;
        [SerializeField] private bool destroyOnFinish = true;
        
        private Vector2 _currentDirection;

        private void Awake()
        {
            _currentDirection = GetDirection(dropperDirection);
        }

        public void DropObject()
        {
            int count = prefabs.Length == 1 ? gameObjectCountToDrop : prefabs.Length; 
            
            for (int i = 0; i < count; i++)
            {
                GameObject prefab = prefabs.Length == 1 ? prefabs[0] : prefabs[i];
                
                GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
                Collider2D c2d = go.GetComponent<Collider2D>();
                Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
                
                if (c2d != null)
                {
                    c2d.isTrigger = false;
                    c2d.enabled = false;
                }
                
                if (rb2d != null)
                {
                    Vector2 direction = (_currentDirection + Random.insideUnitCircle * spreadRadius).normalized;
                    rb2d.AddForce(direction * spreadForce, ForceMode2D.Impulse);
                }
                
                StartCoroutine(EnableColliderAfterDelay(go, c2d, rb2d));
            }
        }
        
        private IEnumerator EnableColliderAfterDelay(GameObject go, Collider2D c2d, Rigidbody2D rb2d)
        {
            
            yield return new WaitForSeconds(0.5f);
            
            if (rb2d != null)
                rb2d.gravityScale = 1f;
            
            if (c2d != null)
            {
                c2d.isTrigger = true;
                c2d.enabled = true;
            }
            
            yield return new WaitForSeconds(3f);
            
            if (destroyOnFinish)
            {
                Destroy(go);
            }
        }

        private Vector3 GetDirection(DropperDirection dir)
        {
            switch (dir)
            {
                case DropperDirection.Left:
                    return Vector3.left;
                case DropperDirection.Right:
                    return Vector3.right;
                case DropperDirection.Top:
                    return Vector3.up;
                case DropperDirection.Bottom:
                    return Vector3.down;
            }
            return Vector3.zero;
        }
        
        // метод срабатывает автоматически, его не нужно помещать в awake
        private bool HasOnePrefab() => prefabs != null && prefabs.Length == 1;
    }
}