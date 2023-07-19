using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Start()
    {
        RotateObject();
    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject.transform);
    }

    void RotateObject()
    {
        float targetRotationY = transform.eulerAngles.y + rotateSpeed;

        transform.DORotate(new Vector3(0f, targetRotationY, 0f), rotateSpeed / 360f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(RotateObject);
    }
}
