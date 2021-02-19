using UnityEngine;
using TMPro;

public class OverlayControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void Start() 
    {
        ScoreTextUpdate();
        ScoreCount.instance.OnScoreUpdate += ScoreTextUpdate;
    }

    private void OnDisable() 
    {
        ScoreCount.instance.OnScoreUpdate -= ScoreTextUpdate;
    }

    private void ScoreTextUpdate()
    {
        score.text = ScoreCount.instance.currentScore.ToString();
    }
}
