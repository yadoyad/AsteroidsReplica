using System;
using UnityEngine;

public class LifeCount : MonoBehaviour
{
    public static LifeCount instance;
    [SerializeField] private int maxLifeAmount = 3;
    public int currentLifeAmount {get; private set;}
    public event Action OnLifeDecrease;

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
        currentLifeAmount = maxLifeAmount;
    }

    public void PlayerGotHit()
    {
        currentLifeAmount--;

        if(currentLifeAmount <= 0)
        {
            OnLifeDecrease?.Invoke();
            GameOver();
        }
        else
        {
            OnLifeDecrease?.Invoke();
        }
    }

    private void GameOver()
    {
        FindObjectOfType<PanelsControl>().GameEnd();
    }

    public void ResetLifeCount()
    {
        currentLifeAmount = maxLifeAmount;
    }
}
