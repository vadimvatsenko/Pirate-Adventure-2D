using UnityEngine;
public class Hero : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 _vDirection;
    
    public void SetDirection(Vector2 dir)
    {
        _vDirection = dir.sqrMagnitude > 1 ? dir.normalized : dir;
    }
    
    public void Update()
    {
        if (_vDirection != Vector2.zero)
        {
            MovementInAllDirections();
        }
    }

    private void MovementInAllDirections()
    {
        Vector2 delta = _vDirection.normalized * (speed * Time.deltaTime);
        Vector2 newPos = new Vector2(transform.position.x + delta.x, transform.position.y + delta.y);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
    
    public void Hit()
    {
        Debug.Log("Hit");
    }
}
