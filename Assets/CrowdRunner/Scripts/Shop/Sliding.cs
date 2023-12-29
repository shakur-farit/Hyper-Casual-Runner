using UnityEngine;
using DG.Tweening;

public class Sliding : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private RectTransform _rectTransform;

    public void FadeOut()
    {
        _rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(-1500f, 0f), _fadeTime, false).SetEase(Ease.InOutQuint);
    }

    public void FadeIn()
    {
        _rectTransform.transform.localPosition = new Vector3(-1500f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.InOutQuint);
    }
}
