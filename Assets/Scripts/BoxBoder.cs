using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoder : MonoBehaviour
{
    public bool endCount;

    public float endTime;

    private void Start()
    {
        endCount = false;
    }

    private void Update()
    {
        if (endCount)
        {
            endTime += Time.deltaTime;

        }
        if (endTime >= 0.1f)
        {
            GameManager.Instance.GameOver();
            endTime = 0;
            endCount = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Drop"))
        {
            endCount = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Drop"))
        {
            endCount = false;
            endTime = 0;
        }
    }
}
