using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform spawnpoint = null;
    [SerializeField] private GameObject spawnEffect = null;
    private float timeSinceSpawned = 0;
    private CinemachineVirtualCamera vcam = null;
    [SerializeField] private GameObject enemy;
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
            SpawnEnemy(enemy);
    }

    public void SpawnEnemy(GameObject enemy)
    {
        var enemyInst = Instantiate(enemy, new Vector2(UnityEngine.Random.Range(-5, 5),UnityEngine.Random.Range(-5, 5)), Quaternion.identity);
        var effectInst = Instantiate(spawnEffect, enemyInst.transform.position, Quaternion.identity);
        effectInst.GetComponent<SpawnEffect>().Init(true);
        Destroy(effectInst, 2f);
    }
    
    public void SpawnPlayer()
    {
        var playerInst = Instantiate(player, spawnpoint.position, spawnpoint.rotation);
        OnSpawnPlayer?.Invoke(this, playerInst);
        var effectInst = Instantiate(spawnEffect, playerInst.transform.position, Quaternion.identity);
        Destroy(effectInst, 2f);
        vcam.Follow = playerInst.transform;
        PlayerController pCont = playerInst.GetComponent<PlayerController>();
        //pCont.FreezeEntity(true);
    }

    private void UnFreezePlayer(PlayerController playerController)
    {
        playerController.FreezeEntity(false);
    }
}
