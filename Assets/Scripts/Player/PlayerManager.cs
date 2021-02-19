using UnityEngine;
using System.Collections;

//Класс для контроля состояний игрока
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private float respawnTime = 1f;
    public bool isDead {get; private set;} = false;
    private BoxCollider boxCollider;
    private SpriteRenderer spriteRenderer;

 
    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() 
    {
        boxCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Asteroid" || other.gameObject.tag == "UFOBullet")
        {
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        FakeDisable();
        LifeCount.instance.PlayerGotHit();

        if(LifeCount.instance.currentLifeAmount > 0)
        {
            StartCoroutine(Ressurection());
        }
    }

    IEnumerator Ressurection()
    {
        yield return new WaitForSeconds(respawnTime);
        isDead = false;
        FakeEnable();
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(respawnTime);
        boxCollider.enabled = true;
    }

    private void FakeDisable()
    {
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;
    }

    private void FakeEnable()
    {
        spriteRenderer.enabled = true;
    }
}
