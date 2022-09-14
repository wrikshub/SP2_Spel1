using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private GameObject scoreCanvas;
    public bool GameHasStarted { get; private set;}

    public event GameStart OnGameStart;
    public delegate void GameStart();

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

    public void FadeOut()
    {
        animator.SetTrigger("quit");
    }

    public void ReloadScene()
    {
        animator.SetTrigger("reload");
    }

    private void ReloadGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
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
        MusicManager.Instance.GameOver();
        player.GetComponent<Health>().OnNoHealth -= GameOver; 
        dCanvas.GameOver(timeSpentAlive, ScoreManager.Instance.Score);
        GameHasStarted = false;
        heartCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
    }

    public void StartGame()
    {
        GameHasStarted = true;
        OnGameStart?.Invoke();
        MusicManager.Instance.GameStarted();
    }
}
