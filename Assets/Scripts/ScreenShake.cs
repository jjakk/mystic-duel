using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f ;
    public float shakeMagnitude = 0.1f ;
    private Vector3 initialPosition ;

    void Start()
    {
        initialPosition = transform.localPosition ;
    }

    public void TriggerShake()
    {
        StartCoroutine( Shake() ) ;
    }

    private IEnumerator Shake()
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
}