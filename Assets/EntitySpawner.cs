using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform spawnpoint = null;
    [SerializeField] private GameObject spawnEffect = null;
    private float timeSinceSpawned = 0;
    private CinemachineVirtualCamera vcam = null;
    public static EntitySpawner Instance { get; private set; }
    
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

    public void SpawnPlayer()
    {
        var playerInst = Instantiate(player, spawnpoint.position, spawnpoint.rotation);
        var effectInst = Instantiate(spawnEffect, playerInst.transform.position, quaternion.identity);
        Destroy(effectInst, 2f);
        vcam.Follow = playerInst.transform;
        PlayerController pCont = playerInst.GetComponent<PlayerController>();
        //playerInst.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360f));
        pCont.FreezeEntity(true);
    }

    private void UnFreezePlayer(PlayerController playerController)
    {
        playerController.FreezeEntity(false);
    }
}
