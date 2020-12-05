using UnityEngine;

public class RoadController : MonoBehaviour
{
    private bool isRoadSizeDown = false;

    private float timer = 0;
    private float sizeDownTime = 2f;

    private Vector3 starRoadtSize;
    private Vector3 finishRoadSize;

    void Start()
    {
        starRoadtSize =transform.localScale;
        finishRoadSize = new Vector3(starRoadtSize.x, starRoadtSize.y, starRoadtSize.z * 0.5f);

        PlayerController.singleton.OnGetKey += RoadSizeDown;
        PlayerController.singleton.OnGetKeyUP += StopRoadSize;
    }
    
    void Update()
    {
        if(isRoadSizeDown)
        {
            transform.localScale = Vector3.Lerp(starRoadtSize, finishRoadSize, timer / sizeDownTime);
            timer += Time.deltaTime;
        }
    }

    private void RoadSizeDown()
    {
        isRoadSizeDown = true;
    }
    
    private void StopRoadSize()
    {
        starRoadtSize = transform.localScale;
        isRoadSizeDown = false;
        timer = 0;
    }
}
