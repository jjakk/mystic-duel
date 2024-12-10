using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController3 : MonoBehaviour
{
    public GameObject distantLightning;

    private void Start()
    {
        StartCoroutine(ToggleObjectWithDelay());
    }

    private IEnumerator ToggleObjectWithDelay()
    {
        yield return new WaitForSeconds(3.0f);
        
        while (true)
        {
            distantLightning.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            distantLightning.SetActive(false);
            yield return new WaitForSeconds(6.0f);


        }
    }
}
