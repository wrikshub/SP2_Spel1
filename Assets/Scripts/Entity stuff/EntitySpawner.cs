using System.Linq;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private float playerTimeInvicible = 2.5f;
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform spawnpoint = null;
    [SerializeField] private GameObject spawnEffect = null;
    [SerializeField] private AudioEvent spawnSound = null;
    private CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject[] enemies;

    private float timeSinceSpawnedLastBatchMax = 1;
    private float timeSinceSpawnedLastBatch = 0;
    [SerializeField] private float enemyRandSpawnRateMax = 20;
    [SerializeField] private float enemyRandSpawnRateMin = 10;
    
    public static EntitySpawner Instance { get; private set; }
    public event ES_SpawnPlayer OnSpawnPlayer;
    public delegate void ES_SpawnPlayer(object sender, GameObject player);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        vcam = Camera.main.GetComponent<CinemachineVirtualCamera>();
        timeSinceSpawnedLastBatchMax = 3;
    }

    private void Update()
    {
        if (!GameManager.Instance.GameHasStarted) return;

        timeSinceSpawnedLastBatch += Time.deltaTime;

        if (timeSinceSpawnedLastBatch > timeSinceSpawnedLastBatchMax)
        {
            SpawnEnemyBatch();
        }
    }


    private void SpawnEnemyBatch()
    {
        timeSinceSpawnedLastBatchMax = Random.Range(enemyRandSpawnRateMin, enemyRandSpawnRateMax);
        
        //Customize this
        int amount = Random.Range(2, 7);
        
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(enemies[Random.Range(0, enemies.Length)], new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(1f, 13f));
        }
        
        timeSinceSpawnedLastBatch = 0;
    }
    
    public Enemy SpawnEnemy(GameObject enemy, Vector2 position)
    {
        var enemyInst = Instantiate(enemy, position, Quaternion.identity);
        var effectInst = Instantiate(spawnEffect, enemyInst.transform.position, Quaternion.identity);
        spawnSound.Play(null, enemy.transform.position);
        Destroy(effectInst, 2f);

        EnemyControl e = enemyInst.GetComponent<EnemyControl>();
        e.Target = GameObject.FindWithTag("Player").transform;
        
        return enemyInst.GetComponent<Enemy>();
    }
    
    public GameObject SpawnPlayer()
    {
        var playerInst = Instantiate(player, spawnpoint.position, spawnpoint.rotation);
        OnSpawnPlayer?.Invoke(this, playerInst);
        var effectInst = Instantiate(spawnEffect, playerInst.transform.position, Quaternion.identity);
        Destroy(effectInst, 2f);
        spawnSound.Play(null, transform.position);
        vcam.Follow = playerInst.transform;
        playerInst.GetComponent<Health>().MakeInvincible(playerTimeInvicible);
        //pCont.FreezeEntity(true);

        return playerInst;
    }

    private void UnFreezePlayer(PlayerController playerController)
    {
        playerController.FreezeEntity(false);
    }
}
