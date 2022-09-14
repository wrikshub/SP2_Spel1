using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSpawner : MonoBehaviour
{
    [SerializeField] private GameObject introEnemy;
    [SerializeField] private float timeUntilSpawnIntroEnemy = 3f;
    
    private void Start()
    {
        Invoke("SpawnIntroEnemy", timeUntilSpawnIntroEnemy);
    }

    private void SpawnIntroEnemy()
    {
        var enemy = EntitySpawner.Instance.SpawnEnemy(introEnemy, Vector3.right * 3.5f);
        enemy.GetComponent<EnemyControl>().Target = null;
    }
}
