using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject gamePanel;


    private void Start()
    {
        PlayerController.singleton.OnDie += Lose;
    }

    private void Lose()
    {
        gameOverPanel.SetActive(true);
    }
}
