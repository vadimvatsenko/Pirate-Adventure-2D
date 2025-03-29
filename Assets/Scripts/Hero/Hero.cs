using UnityEngine;
public class Hero : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Vector2 _vDirection;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 dir)
    {
        _vDirection = dir;
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_vDirection.x * speed, _vDirection.y);
    }
    
    public void Jump()
    {
        Debug.Log("Jump");
        _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode2D.Impulse);
    }
}
