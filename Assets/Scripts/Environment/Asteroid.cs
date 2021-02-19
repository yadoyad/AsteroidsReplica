using UnityEngine;

public enum AsteroidType {Big, Medium, Small}
[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField] private AsteroidType myType = AsteroidType.Big;
    [SerializeField] private float minStartingSpeed = 10f;
    [SerializeField] private float maxStartingSpeed = 50f;
    [SerializeField] private float maxCruisingSpeed = 3f;
    [SerializeField] private float maxRotation = 30f;
    [SerializeField] private int scoreValue = 20;

    [Header("Варианты спрайтов")]
    [SerializeField] private Sprite asteroid1;
    [SerializeField] private Sprite asteroid2;
    [SerializeField] private Sprite asteroid3;

    [Header("Вспомогательные элементы")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject mediumAsteroidPrefab;
    [SerializeField] private GameObject smallAsteroidPrefab;
    private Rigidbody rb;
    private float rotationZ;
    //Модульные значения ширины и высоты поля от центра до края
    private const float fieldDimensionX = 6.5f;
    private const float fieldDimensionY = 4.5f;

    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();
        SpriteSetup();  
    }

    private void Start() 
    {
        RotationSetup(); //Метод запускает цепную реакцию, которая устанавливает все необходимые физические значения, не только ротацию
    }

    private void FixedUpdate() 
    {
        transform.Rotate(new Vector3(0, 0, rotationZ) * Time.deltaTime);
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxCruisingSpeed, maxCruisingSpeed),
            Mathf.Clamp(rb.velocity.y, -maxCruisingSpeed, maxCruisingSpeed));
    }
    #region Setups
    private void SpriteSetup()
    {
        var spriteID = Random.Range(0, 3);

        switch(spriteID)
        {
            case 0:
                spriteRenderer.sprite = asteroid1;
                break;
            case 1:
                spriteRenderer.sprite = asteroid2;
                break;
            case 2:
                spriteRenderer.sprite = asteroid3;
                break;
            default:
                spriteRenderer.sprite = asteroid1;
                break;
        }
    }

    private void RotationSetup()
    {
        rotationZ = Random.Range(-maxRotation, maxRotation);

        PositionSetup();
    }

    private void PositionSetup()
    {
        var positionX = transform.position.x;
        var positionY = transform.position.y;

        if(myType == AsteroidType.Big)
        {
            positionX = Random.Range(-fieldDimensionX, fieldDimensionX);
            positionY = Random.Range(-fieldDimensionY, fieldDimensionY);

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
        else
        {
            Vector3 currentPosition = new Vector3 (positionX, positionY, 0);
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
    #endregion

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Bullet")
        {
            SpawnLesserAsteroids();
            AddScore();
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player")
        {
            SpawnLesserAsteroids();
            Destroy(gameObject);
        }
    }

    private void AddScore()
    {
        ScoreCount.instance.ScoreUpdate(scoreValue);
    }

    private void SpawnLesserAsteroids()
    {
        if(myType == AsteroidType.Big)
        {
            var first = Instantiate(mediumAsteroidPrefab, transform.position, Quaternion.identity);
            var second = Instantiate(mediumAsteroidPrefab, transform.position, Quaternion.identity);
        }
        else if(myType == AsteroidType.Medium)
        {
            var first = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
            var second = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        }
    }
}
