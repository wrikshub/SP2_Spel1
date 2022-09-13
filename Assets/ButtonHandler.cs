using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string mouseHover = "hover";
    [SerializeField] private string mouseClick = "click";
    [SerializeField] private AudioEvent clickSound = null;
    [SerializeField] private AudioEvent hoverSound = null;

    public void MouseHovers()
    {
        hoverSound.Play();
        animator.SetTrigger(mouseHover);
    }

    public void MouseClicked()
    {
        clickSound.Play();
        animator.SetTrigger(mouseClick);
    }

    public void StartLeaveGame()
    {
        GameManager.Instance.Quit();
    }
}
