using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public static ScoreManager Instance { get; private set; }

    public event UpdateScore OnUpdateScore;

    public delegate void UpdateScore(int amount);

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

    public void AddScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        OnUpdateScore?.Invoke(scoreToAdd);
    }
}