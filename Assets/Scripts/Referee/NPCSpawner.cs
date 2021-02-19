using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner instance;
    [Header("Префабы")]
    [SerializeField] private GameObject asteroidBig;
    [SerializeField] private GameObject UFOPrefab;
    [Header("Параметры спавна")]
    [SerializeField] private float asteroidAppearTimeSeconds = 10f;
    [SerializeField] private float ufoAppearTimeSeconds = 30f;
    [SerializeField] private int initialAsteroidAmount = 5;
    private float lastAstrdSpawnTime = 0;
    private float lastUFOSpawnTime = 0;
    private List<GameObject> npcs = new List<GameObject>();
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

    private void Start() 
    {
        npcs.Clear();
        SpawnAtStart();
    }

    private void SpawnAtStart()
    {
        for (int i = 0; i < initialAsteroidAmount; i++)
        {
            var astrd = Instantiate(asteroidBig, Vector3.zero, Quaternion.identity);
        }

        lastAstrdSpawnTime = Time.time;
        lastUFOSpawnTime = Time.time;
        StartCoroutine(AstrdSpawnRoutine());
        StartCoroutine(UFOSpawnRoutine());
    }

    private void SpawnAsteroid()
    {
        var astrd = Instantiate(asteroidBig, Vector3.zero, Quaternion.identity);
        lastAstrdSpawnTime = Time.time;
        StartCoroutine(AstrdSpawnRoutine());
    }

    private void SpawnUFO()
    {
        var ufo = Instantiate(UFOPrefab, Vector3.zero, Quaternion.identity);
        lastUFOSpawnTime = Time.time;
        StartCoroutine(UFOSpawnRoutine());
    }

    IEnumerator UFOSpawnRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        if(Time.time >= lastUFOSpawnTime + ufoAppearTimeSeconds)
        {
            SpawnUFO();
        }
        else
        {
            StartCoroutine(UFOSpawnRoutine());
        }
    }

    IEnumerator AstrdSpawnRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        if(Time.time >= lastAstrdSpawnTime + asteroidAppearTimeSeconds)
        {
            SpawnAsteroid();
        }
        else
        {
            StartCoroutine(AstrdSpawnRoutine());
        }
    }
}
