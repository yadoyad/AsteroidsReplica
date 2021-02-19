using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("Настройки пули")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    private Rigidbody rb;

    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);    
    }

    private void Start() 
    {
        rb.velocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }
}
