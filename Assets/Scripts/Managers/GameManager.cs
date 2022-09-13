using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Animator animator = null;
    [SerializeField] private string animParam = "hurt";
    private float timeSpentAlive = 0f;
    private bool countTimeSpentAlive = false;
    private GameObject player;
    [SerializeField] private DeathCanvas dCanvas;
    [SerializeField] private GameObject heartCanvas;
    
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
    }

    private void Update()
    {
        if (!countTimeSpentAlive) return;

        timeSpentAlive += Time.deltaTime;
    }

    public void HurtEffect()
    {
        animator.SetTrigger(animParam);
    }

    public void Quit()
    {
        animator.SetTrigger("quit");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    
    public void SpawnPlayer()
    {
        countTimeSpentAlive = true;
        player = EntitySpawner.Instance.SpawnPlayer();
        player.GetComponent<Health>().OnNoHealth += GameOver;
    }

    private void GameOver(object sender, DamageArgs damageargs)
    {
        player.GetComponent<Health>().OnNoHealth -= GameOver; 
        dCanvas.GameOver(timeSpentAlive);
        heartCanvas.SetActive(false);
    }
}
