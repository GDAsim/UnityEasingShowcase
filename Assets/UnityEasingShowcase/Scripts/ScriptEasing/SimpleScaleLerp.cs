using UnityEngine;

public class SimpleScaleLerp : MonoBehaviour
{
    public Easing.EasingType easingType = Easing.EasingType.Linear;

    Vector3 startPos;
    Vector3 startScale;
    Vector3 endScale;

    void Awake()
    {
        startPos = transform.localPosition;
        startScale = Vector3.one;
        endScale = startScale + new Vector3(0, 5, 0);
    }
    void Update()
    {
        var pingpongt = Mathf.PingPong(Time.time, 1);

        var t = Easing.ApplyEasing(easingType, pingpongt);

        var newScale = Vector3.LerpUnclamped(startScale, endScale, t);
        var scalediff = newScale - startScale;

        transform.localScale = newScale;
        transform.localPosition = startPos + scalediff / 2;
    }
}
