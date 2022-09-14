using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Animator animator;

    public void GameOver(float timeSpentAlive, int score)
    {
        animator.SetTrigger("killed");
        deathText.text = $"YOU LIVED FOR {Mathf.Ceil(timeSpentAlive)} SECONDS";
        scoreText.text = $"SCORE: {score.ToString()}";
    }
}
