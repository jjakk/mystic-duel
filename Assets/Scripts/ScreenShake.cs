using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f ;
    public float shakeMagnitude = 0.1f ;
    private Vector3 initialPosition ;
    private Coroutine continuousShakeCoroutine;

    void Start()
    {
        initialPosition = transform.localPosition ;
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    public void StartContinuousShake(float magnitude)
    {
        if (continuousShakeCoroutine != null)
        {
            StopCoroutine(continuousShakeCoroutine);
        }
        
        continuousShakeCoroutine = StartCoroutine(ContinuousShake(magnitude));
    }

    public void StopContinuousShake()
    {
        if (continuousShakeCoroutine != null)
        {
            StopCoroutine(continuousShakeCoroutine);
            continuousShakeCoroutine = null;
        }
        transform.localPosition = initialPosition;
    }


    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f ;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude ;
            float y = Random.Range(-1f, 1f) * shakeMagnitude ;

            transform.localPosition = new Vector3(x, y, initialPosition.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;
    }

    private IEnumerator ContinuousShake(float magnitude)
    {
        while (true)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, initialPosition.z);

            yield return null;
        }
    }
}