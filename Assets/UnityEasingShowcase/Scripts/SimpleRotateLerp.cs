using UnityEngine;

public class SimpleRotateLerp : MonoBehaviour
{
    public EasingFunctions.EasingType easingType = EasingFunctions.EasingType.Linear;

    Quaternion startRot;
    Quaternion endRot;

    void Awake()
    {
        startRot = Quaternion.Euler(0, 0, 0);
        endRot = Quaternion.Euler(0, -180, 0);
    }
    void Update()
    {
        var pingpongt = Mathf.PingPong(Time.time, 1);

        var t = EasingFunctions.ApplyEasing(easingType, pingpongt);

        transform.rotation = Quaternion.LerpUnclamped(startRot, endRot, t);
    }
}
