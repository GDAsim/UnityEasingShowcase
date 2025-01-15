using UnityEngine;

public class SimpleMoveLerp : MonoBehaviour
{
    [SerializeField, HideInInspector] Vector3 Delta = new Vector3(10, 0, 0);
    public EasingFunctions.EasingType easingType = EasingFunctions.EasingType.Linear;

    Vector3 startPos;
    Vector3 endPos;

    void Awake()
    {
        startPos = transform.localPosition;
        endPos = startPos + Delta;
    }
    void Update()
    {
        var pingpongt = Mathf.PingPong(Time.time, 1);

        var t = EasingFunctions.ApplyEasing(easingType, pingpongt);

        transform.localPosition = Vector3.LerpUnclamped(startPos, endPos, t);
    }
}
