using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private string scoreText = "TOTAL SCORE: ";

    private void OnEnable() 
    {
        score.text = scoreText + ScoreCount.instance.currentScore.ToString();
    }
}
