using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class HUDManager : MonoBehaviour
{
    private ScoreManager score;
    [SerializeField] private GameObject scorePopUp = null;
    [SerializeField] private Vector2 spawnLocation = Vector2.zero;
    [SerializeField] private Vector2 randomBetween = Vector2.one;
    [SerializeField] private Animator anim;
    [SerializeField] private string scoreParam = "increasedScore";
    private Canvas canvas;
    
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        score = ScoreManager.Instance;
        score.OnUpdateScore += ScoreReceived;
    }

    private void OnDestroy()
    {
        score.OnUpdateScore -= ScoreReceived;
    }

    private void ScoreReceived(int amount)
    {
        anim.SetTrigger(scoreParam);
        
        //var inst = Instantiate(scorePopUp, spawnLocation, quaternion.identity, canvas.transform);
        //inst.GetComponent<TextMeshPro>().text = amount.ToString();
        //Destroy(inst, 2);
    }
}
