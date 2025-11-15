using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SetupRotateObjects_Shader : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;

    void Awake()
    {
        ApplyEasingToGameObjects();
    }

    void Update()
    {
        var pingpongt = Mathf.PingPong(Time.time, 1);

        var enumNames = Enum.GetNames(typeof(Easing.EasingType));
        var enumCount = enumNames.Length;
        for (int i = 0; i < gameObjects.Length && i < enumCount; i++)
        {
            var mat = gameObjects[i].GetComponent<MeshRenderer>().sharedMaterial;
            mat.SetFloat("_LerpTime", pingpongt);
        }
    }

    [ContextMenu("Apply")]
    void ApplyEasingToGameObjects()
    {
        var random = new Random(1);
        var enumNames = Enum.GetNames(typeof(Easing.EasingType));
        var enumCount = enumNames.Length;
        for (int i = 0; i < gameObjects.Length && i < enumCount; i++)
        {

#if UNITY_EDITOR
            var mat = gameObjects[i].GetComponent<MeshRenderer>().material;
#else
            var mat = gameObjects[i].GetComponent<MeshRenderer>().sharedMaterial;
#endif
            gameObjects[i].transform.rotation = Quaternion.Euler(Vector3.zero);
            var end = Quaternion.Euler(0, -180, 0);

            mat.SetVector("_EndRotation", new Vector4(end.x, end.y, end.z, end.w));
            mat.SetInt("_EasingType", i);

            gameObjects[i].transform.localPosition = new Vector3(0f, 0f, i * 4);
            gameObjects[i].name = enumNames[i];

            mat.color = new Color(random.NextFloat(0, 1), random.NextFloat(0, 1), random.NextFloat(0, 1));
        }
    }
}
