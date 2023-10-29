using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [SerializeField] private CanvasGroup _fadeCanvas;
    [SerializeField] private float _fadeTime = 1f;

    public float FadeTime => _fadeTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool isIn)
    {
        _fadeCanvas.gameObject.SetActive(true);

        float targetValue = isIn ? 1f : 0f;
        float startValue = targetValue == 1f ? 0f : 1f;

        _fadeCanvas.alpha = startValue;

        yield return new WaitForSeconds(0.1f);

        for (float t = 0f; t < _fadeTime; t += Time.deltaTime)
        {
            _fadeCanvas.alpha = Mathf.Lerp(startValue, targetValue, t / _fadeTime);
            yield return null;
        }

        _fadeCanvas.alpha = targetValue;

        if (isIn == false)
        {
            _fadeCanvas.gameObject.SetActive(false);
        }
    }
}
