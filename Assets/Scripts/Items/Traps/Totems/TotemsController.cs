using System.Collections;
using UnityEngine;

namespace Items.Traps.Totems
{
    public class TotemsController : MonoBehaviour
    {
        private TotemTrap[] _totemsElements;

        private void Awake()
        {
            _totemsElements = FindObjectsOfType<TotemTrap>();
        }

        public void TotemsFlip() => StartCoroutine(TotemsFlipRoutine());

        private IEnumerator TotemsFlipRoutine()
        {
            foreach (var to in _totemsElements)
            {
                yield return new WaitForSeconds(2);
                to.Flip();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                
            }
        }
    }
}