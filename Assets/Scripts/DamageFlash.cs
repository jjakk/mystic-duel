using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private Coroutine _damageFlashCoroutine;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        Init();
    }

    private void Init()
    {
        _materials = new Material [_spriteRenderers.Length];

        for( int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    public void CallDamageFlash()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        // set the color
        SetFlashColor();

        // set flash amount
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _flashTime)
        {
            // iterate elapsedTime
            elapsedTime += Time.deltaTime;

            // lerp flash amount
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for( int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_FlashColor", _flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        // set flash amount
        for( int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetFloat("_FlashAmount", amount);
        }
    }

    
}
