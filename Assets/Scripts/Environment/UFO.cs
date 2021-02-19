using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFO : MonoBehaviour
{
    [SerializeField] private float minStartingSpeed = 10f;
    [SerializeField] private float maxStartingSpeed = 50f;
    [SerializeField] private float maxCruisingSpeed = 3f;
    [SerializeField] private float maxRotation = 30f;
    [SerializeField] private int scoreValue = 20;
    private Rigidbody rb;
    private float rotationZ;
    //Модульные значения ширины и высоты поля от центра до края
    private const float fieldDimensionX = 6.5f;
    private const float fieldDimensionY = 4.5f;
    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start() 
    {
        RotationSetup();
    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxCruisingSpeed, maxCruisingSpeed),
            Mathf.Clamp(rb.velocity.y, -maxCruisingSpeed, maxCruisingSpeed));
    }

    private void RotationSetup()
    {
        rotationZ = Random.Range(-maxRotation, maxRotation);

        PositionSetup();
    }

    private void PositionSetup()
    {
        var positionX = Random.Range(-fieldDimensionX, fieldDimensionX);
        var positionY = Random.Range(-fieldDimensionY, fieldDimensionY);

        Vector3 currentPosition = new Vector3 (positionX, positionY, 0);
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position; 

        float dist = Vector3.Distance(currentPosition, playerPosition);

        if(Mathf.Abs(dist) < 2.5f)
            PositionSetup();
        else
        {
            gameObject.transform.position = currentPosition;
            AccelerationSetup();
        }
    }

    private void AccelerationSetup()
    {
        float speedX = Random.Range(minStartingSpeed, maxStartingSpeed);
        var randomizerX = TossACoin();
        speedX = randomizerX == 1 ? speedX * -1 : speedX;
        rb.AddForce(transform.right * speedX);

        float speedY = Random.Range(minStartingSpeed, maxStartingSpeed);
        var randomizerY = TossACoin();
        speedY = randomizerY == 1 ? speedY * -1 : speedY;
        rb.AddForce(transform.up * speedY);
    }
    private int TossACoin() => Random.Range(0, 2);

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Bullet")
        {
            AddScore();
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void AddScore()
    {
        ScoreCount.instance.ScoreUpdate(scoreValue);
    }
}
