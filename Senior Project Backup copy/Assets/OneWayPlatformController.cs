using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime = 0.1f;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartCoroutine(FallThrough());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0;
        }
    }

    IEnumerator FallThrough()
    {
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(waitTime);
        effector.rotationalOffset = 0;
    }
}

