using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform spawnpoint = null;
    [SerializeField] private GameObject spawnEffect = null;
    [SerializeField] private float spawnTimer = 1f;
    private float timeSinceSpawned = 0;
    private CinemachineVirtualCamera vcam = null;
    public static GameManager Instance { get; private set; }
    
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
        //var effectInst = Instantiate(spawnEffect, playerInst.transform.position, quaternion.identity);
        //Destroy(effectInst, 2f);
        vcam.Follow = playerInst.transform;
        //PlayerController pCont = playerInst.GetComponent<PlayerController>();
        //pCont.FreezeEntity(true);
    }

    private void UnFreezePlayer(PlayerController playerController)
    {
        playerController.FreezeEntity(false);
    }

}
