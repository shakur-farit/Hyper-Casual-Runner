using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sliding : MonoBehaviour
{
    public float fadeTiem = 1f;
    public RectTransform rectTransform;

    public void FadeOut()
    {
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(-1500f, 0f), fadeTiem, false).SetEase(Ease.InOutQuint);
    }

    public void FadeIn()
    {
        rectTransform.transform.localPosition = new Vector3(-1500f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTiem, false).SetEase(Ease.InOutQuint);
    }
}
