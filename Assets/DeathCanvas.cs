using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private Animator animator;

    public void GameOver(float timeSpentAlive)
    {
        animator.SetTrigger("killed");
        deathText.text = timeSpentAlive.ToString();
    }
}
