using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("SpawnRate")]
    [SerializeField] private float spawnRate = 5f;
    private float spawnTimer;

    [Header("Spawner")]
    [SerializeField] public Transform spawner;


    [SerializeField] private GameObject prefab;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        //Spawner();
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnRate)
        {
            GameObject enemy = PoolManager.Instance.GetPooledObject("Enemies", spawner.position, prefab.transform.rotation);
            enemy.transform.position = spawner.position;
            enemy.SetActive(true);

            spawnTimer = 0f;
        }
    }
}
