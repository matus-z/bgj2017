using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public float ShakeDuration = 0.5f;
    public float ShakeAmount = 0.1f;

    private Vector3 OriginalPos;

    private float ShakeProgress = 0f;

    // ------------------------------------------------------------------------------------------------------------------
    private void OnEnable()
    {
        OriginalPos = Camera.main.transform.localPosition;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void StartNow()
    {
        ShakeProgress = ShakeDuration;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (ShakeProgress > 0)
        {
            Vector3 shakeVec = Random.insideUnitCircle;
            Camera.main.transform.localPosition = OriginalPos + shakeVec * ShakeAmount;
            ShakeProgress -= Time.deltaTime * 1.0f;
        }
        else
        {
            ShakeProgress = 0f;
            Camera.main.transform.localPosition = OriginalPos;
        }
    }
}
