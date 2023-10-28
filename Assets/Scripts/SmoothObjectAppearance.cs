using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothObjectAppearance : MonoBehaviour
{
    private const float ChangeAlphaDur = 1f;
    public void AppearObject(GameObject obj)
    {
        CanvasGroup cg;

        if (!obj.TryGetComponent(out cg))
        {
            obj.AddComponent(typeof(CanvasGroup));
            cg = obj.GetComponent<CanvasGroup>();
        }

        StartCoroutine(ChangeAlphaCor(cg, true));
    }

    public void DisappearObject(GameObject obj)
    {
        CanvasGroup cg;

        if (!obj.TryGetComponent(out cg))
        {
            try
            {
                obj.AddComponent(typeof(CanvasGroup));
                cg = obj.GetComponent<CanvasGroup>();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        StartCoroutine(ChangeAlphaCor(cg, false));
    }

    private IEnumerator ChangeAlphaCor(CanvasGroup cg, bool isAppear)
    {
        if (isAppear == true)
        {
            cg.gameObject.SetActive(true);
        }

        float targetValue = isAppear ? 1f : 0f;
        float startValue = targetValue == 1f ? 0f : 1f;

        cg.alpha = startValue;

        for (float t = 0f; t < ChangeAlphaDur; t += Time.deltaTime)
        {
            cg.alpha = Mathf.Lerp(startValue, targetValue, t / ChangeAlphaDur);
            yield return null;
        }

        cg.alpha = targetValue;

        if (isAppear == false)
        {
            cg.gameObject.SetActive(false);
        }
    }
}
