using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки управления")]
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float maximumSpeed = 5f;
    private Rigidbody rb;

    private void OnEnable() 
    {
         rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        if(!PlayerManager.instance.isDead)
        {
            Movement();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Movement()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        rb.AddForce(transform.up * acceleration * Input.GetAxis("Vertical"));
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maximumSpeed, maximumSpeed), 
            Mathf.Clamp(rb.velocity.y, -maximumSpeed, maximumSpeed));
    }
}
