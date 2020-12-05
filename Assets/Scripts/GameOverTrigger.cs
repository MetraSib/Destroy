using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    readonly static string player = "Player";


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player)) 
        {
            winPanel.SetActive(true);
        }
    }
}
