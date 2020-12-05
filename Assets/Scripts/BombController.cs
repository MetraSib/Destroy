using UnityEngine;

public class BombController : MonoBehaviour
{
    private bool isBombLaunched = false;
    private bool isBombGrowingUp = false;

    private Vector3 startPositionOffset;
    private Vector3 startSize;
    private Vector3 finishSize;

    private MeshRenderer meshRenderer;

    private float timer=0;
    private float bombSpeed = 9;
    private float sizeUpTime = 3;
    private float explosionRadius = 0.7f;

    private CapsuleCollider [] enemies;

    private void Start()
    {
        enemies = FindObjectsOfType<CapsuleCollider>();

        startSize = transform.localScale;
        finishSize = startSize * 3;
        startPositionOffset = transform.position - transform.parent.position;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        PlayerController.singleton.OnGetKeyUP += FalseGrowing;
        PlayerController.singleton.OnGetKey += TrueGrowing;
    }

    void Update()
    {
        if(isBombLaunched) 
        {
            transform.Translate(Vector3.left * bombSpeed * Time.deltaTime);
        }

        if (isBombGrowingUp) 
        {
            transform.localScale = Vector3.Lerp(startSize, finishSize, timer / sizeUpTime);
            explosionRadius += (Time.deltaTime / sizeUpTime);
            explosionRadius = Mathf.Clamp(explosionRadius, 0.7f, 2f);
            timer += Time.deltaTime;
        }
    }

    private void FalseGrowing()
    {
        isBombGrowingUp = false;
        isBombLaunched = true;
        timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) <= explosionRadius)
            {
                enemies[i].gameObject.SetActive(false);
            }
        }

        meshRenderer.enabled = false;
        isBombLaunched = false;
        transform.position = transform.parent.position + startPositionOffset;
        explosionRadius = 0.5f;
    }

    private void TrueGrowing() 
    {
        isBombGrowingUp = true;
        meshRenderer.enabled = true;
    }
}
