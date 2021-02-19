using System;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public static ScoreCount instance;
    public int currentScore {get; private set;}
    public event Action OnScoreUpdate;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start() 
    {
        currentScore = 0;
    }

    public void ScoreUpdate(int value)
    {
        currentScore += value;
        OnScoreUpdate?.Invoke();
    }
}
