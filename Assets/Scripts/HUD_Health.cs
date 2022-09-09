using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Health : MonoBehaviour
{
    [SerializeField] private GameObject healthIcon = null;
    [SerializeField] private GameObject holder = null;
    [SerializeField] private float width = 10f;
    [SerializeField] private float padding = 10f;
    [SerializeField] private string heartDamageTag = "damaged";
    private List<GameObject> activeIcons = new List<GameObject>();
    private int timesTakenDamage = 0;
    private float middleOfScreenCoords = 0;
    private List<Vector2> spawnCoords = new List<Vector2>();
    private int lives = 5;
    [SerializeField] private float spawnTimer = 0.125f;
    private float currentTimer = 0f;
    private int timesSpawnedIndex = 0;
    private float totalDist = 0f;

    private Health playerHealth = null;
    private bool playerHasSpawned = false;

    private void Start()
    {
        EntitySpawner.Instance.OnSpawnPlayer += PlayerSpawned;
    }

    private void PlayerSpawned(object sender, GameObject g)
    {
        playerHealth = g.GetComponent<Health>();
        playerHealth.OnTakeDamage += OnTakeDamage;
        lives = playerHealth.CurrentHealth;
        
        currentTimer = spawnTimer;

        playerHasSpawned = true;
        
        for (int i = 1; i < lives + 1; i++)
        {
            float pos = i * width + i * padding;
            
            totalDist += (width + padding);
            spawnCoords.Add(new Vector2(pos, 0));
        }

        for (int i = 0; i < spawnCoords.Count; i++)
        {
            spawnCoords[i] -= new Vector2((totalDist / 2) + (padding + width) / 2, 0);
        }
    }
    
    private void OnDestroy()
    {
        playerHealth.OnTakeDamage -= OnTakeDamage;
        EntitySpawner.Instance.OnSpawnPlayer -= PlayerSpawned;
    }

    private void Update()
    {
        if (!playerHasSpawned) return;
        
        if(Input.GetKeyDown(KeyCode.L))
            OnTakeDamage(null, null);
        
        currentTimer += Time.deltaTime;
        
        if (timesSpawnedIndex < lives && currentTimer >= spawnTimer)
        {
            var heart = Instantiate(healthIcon, Vector3.zero, Quaternion.identity, holder.transform);
            heart.transform.localPosition = spawnCoords[timesSpawnedIndex];
            
            activeIcons.Add(heart);
            
            currentTimer = 0;
            timesSpawnedIndex++;
            if (timesSpawnedIndex == lives)
            {
                activeIcons.Reverse();
            }
        }
    }
    
    private void OnTakeDamage(object sender, DamageArgs args)
    {
        if (!playerHasSpawned || timesTakenDamage >= lives) return;
        
        timesTakenDamage++;
        activeIcons[timesTakenDamage - 1].GetComponent<Animator>().SetTrigger(heartDamageTag);
    }
}
