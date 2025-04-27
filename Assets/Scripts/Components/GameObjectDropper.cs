using System.Collections;
using Items.Coins;
using UnityEngine;

namespace Components
{
    public class GameObjectDropper : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int gameObjectCountToDrop = 10;
        [SerializeField] private float spreadForce = 1.5f;

        public void DropObject()
        {
            for (int i = 0; i < gameObjectCountToDrop; i++)
            {
                GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
                Collider2D c2d = go.GetComponent<Collider2D>();
                Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();
                
                if (c2d != null)
                {
                    c2d.enabled = false;
                    c2d.isTrigger = false;
                }
                
                if (rb2d != null)
                {
                    
                    Vector2 direction = (Vector2.up + Random.insideUnitCircle * 0.5f).normalized;
                    rb2d.AddForce(direction * spreadForce, ForceMode2D.Impulse);
                }
                
                StartCoroutine(EnableColliderAfterDelay(go, c2d, rb2d));
            }
        }

        private IEnumerator EnableColliderAfterDelay(GameObject go, Collider2D c2d, Rigidbody2D rb2d)
        {
            yield return new WaitForSeconds(0.5f);
            rb2d.gravityScale = 1f;
            if (c2d != null)
            {
                c2d.enabled = true;
                c2d.isTrigger = true;
            }
            
            yield return new WaitForSeconds(3f);
            Destroy(go);
        }
    }
}