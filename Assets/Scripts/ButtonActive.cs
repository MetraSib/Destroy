using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    [SerializeField] private GameObject button;

    private void OnCollisionEnter(Collision collision)
    {
        button.SetActive(true);
    }
}
