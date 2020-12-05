using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton { get; private set; }

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
    }

    private float timer = 0;
    private float sizeDownTime = 2f;

    public delegate void Bomb();

    public event Bomb OnGetKeyUP;
    public event Bomb OnGetKey;
    public event Bomb OnDie;

    private Vector3 startSize;
    private Vector3 finishSize;

    private Rigidbody playerRB;

    private bool isOnGround = true;

    readonly static string road = "Road";

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();

        startSize = transform.localScale;
        finishSize = startSize * 0.5f;

        StartCoroutine(DieWatcher());
    }

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:

                    OnGetKey?.Invoke();

                    break;

                case TouchPhase.Stationary:

                    transform.localScale = Vector3.Lerp(startSize, finishSize, timer / sizeDownTime);
                    timer += Time.deltaTime;

                    break;

                case TouchPhase.Ended:

                    OnGetKeyUP?.Invoke();
                    startSize = transform.localScale;
                    timer = 0;

                    break;
            }
        }

#endif

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnGetKey?.Invoke();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.localScale = Vector3.Lerp(startSize, finishSize, timer / sizeDownTime);

            timer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && timer != 0)
        {
            OnGetKeyUP?.Invoke();
            startSize = transform.localScale;
            timer = 0;
        }
#endif
    }

    IEnumerator DieWatcher()
    {
        while (true)
        {
            if (transform.localScale == finishSize)
            {
                OnDie?.Invoke();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(road))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(road))
        {
            isOnGround = false;
        }
    }

    public void ButtonPressed()
    {
        if (isOnGround)
        {
            playerRB.AddForce( (Vector3.up * 3)+(Vector3.left * 2), ForceMode.Impulse);
        }
    }
}
