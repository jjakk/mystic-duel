using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController2 : MonoBehaviour
{
    public GameObject smallLightning;

    private void Start()
    {
        StartCoroutine(ToggleObjectWithDelay());
    }

    private IEnumerator ToggleObjectWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        
        while (true)
        {
            smallLightning.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            smallLightning.SetActive(false);
            yield return new WaitForSeconds(5.0f);


        }
    }
}
