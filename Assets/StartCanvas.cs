using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        GameManager.Instance.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= OnGameStart;
    }


    private void OnGameStart()
    {
        animator.SetTrigger("vanish");
    }
}
