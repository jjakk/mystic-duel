using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController1 : MonoBehaviour
{
    public GameObject largeLightning;

    private void Start()
    {
        StartCoroutine(ToggleObjectWithDelay());
    }

    private IEnumerator ToggleObjectWithDelay()
    {
        while (true)
        {
            largeLightning.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            largeLightning.SetActive(false);
            yield return new WaitForSeconds(3.0f);


        }
    }
}
