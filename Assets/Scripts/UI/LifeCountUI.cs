using System.Collections.Generic;
using UnityEngine;

public class LifeCountUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> lifeImages = new List<GameObject>();
    private int lifeCounter = 0;

    private void Start() 
    {
        LifeCount.instance.OnLifeDecrease += RemoveLife;
    }

    private void OnDisable() 
    {
        LifeCount.instance.OnLifeDecrease -= RemoveLife;
    }
    private void RemoveLife()
    {
        if(lifeCounter < lifeImages.Count)
        {
            lifeImages[lifeCounter].SetActive(false);
            lifeCounter++;
        }
    }
}
