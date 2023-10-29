using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCaller : MonoBehaviour
{
    public void FadeIn()
    {
        FadeController.Instance.FadeIn();
    }

    public void FadeOut()
    {
        FadeController.Instance.FadeOut();
    }
}
