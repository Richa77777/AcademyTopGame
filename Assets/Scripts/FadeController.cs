using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [SerializeField] private CanvasGroup _fadeCanvas;

    private const float FadeTime = 1f;

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
        if (isIn == true)
        {
            _fadeCanvas.gameObject.SetActive(true);
        }

        float targetValue = isIn ? 1f : 0f;
        float startValue = targetValue == 1f ? 0f : 1f;

        _fadeCanvas.alpha = startValue;

        for (float t = 0f; t < FadeTime; t += Time.deltaTime)
        {
            _fadeCanvas.alpha = Mathf.Lerp(startValue, targetValue, t / FadeTime);
            yield return null;
        }

        _fadeCanvas.alpha = targetValue;

        if (isIn == false)
        {
            _fadeCanvas.gameObject.SetActive(false);
        }
    }
}
