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
    private float timeSinceSpawned = 0;
    private CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject[] enemies;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SpawnEnemy(enemies[Random.Range(0, enemies.Length)]);
        }
    }

    public void SpawnEnemy(GameObject enemy)
    {
        var enemyInst = Instantiate(enemy, new Vector2(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5)), Quaternion.identity);
        var effectInst = Instantiate(spawnEffect, enemyInst.transform.position, Quaternion.identity);
        spawnSound.Play(null, enemy.transform.position);
        Destroy(effectInst, 2f);
    }
    
    public void SpawnPlayer()
    {
        var playerInst = Instantiate(player, spawnpoint.position, spawnpoint.rotation);
        OnSpawnPlayer?.Invoke(this, playerInst);
        var effectInst = Instantiate(spawnEffect, playerInst.transform.position, Quaternion.identity);
        Destroy(effectInst, 2f);
        spawnSound.Play(null, transform.position);
        vcam.Follow = playerInst.transform;
        playerInst.GetComponent<Health>().MakeInvincible(playerTimeInvicible);
        //pCont.FreezeEntity(true);
    }

    private void UnFreezePlayer(PlayerController playerController)
    {
        playerController.FreezeEntity(false);
    }
}
