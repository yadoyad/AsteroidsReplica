using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsControl : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject endGamePanel;

    private void Start() 
    {
        Time.timeScale = 1;
        overlay.SetActive(true);
        endGamePanel.SetActive(false);
    }
    
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameEnd()
    {
        overlay.SetActive(false);
        endGamePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
