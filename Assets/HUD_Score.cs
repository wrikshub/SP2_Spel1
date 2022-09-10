using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Score : MonoBehaviour
{
    private ScoreManager score;
    [SerializeField] private Animator anim;
    [SerializeField] private string scoreParam = "increasedScore";
    [SerializeField] private TextMeshProUGUI text;
    
    private void Start()
    {
        score = ScoreManager.Instance;
        score.OnUpdateScore += ScoreReceived;
    }

    private void OnDestroy()
    {
        score.OnUpdateScore -= ScoreReceived;
    }

    private void ScoreReceived(int amount)
    {
        text.gameObject.SetActive(true);
        anim.SetTrigger(scoreParam);
        text.text = score.Score.ToString();
    }
}
