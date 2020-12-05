using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animation doorAnimation;

    readonly static string player = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(player)) 
        {
            doorAnimation.Play();
        }
    }
}
