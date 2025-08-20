using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SetupScaleObjects : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;

    void Awake()
    {
        ApplyEasingToGameObjects();
    }

    [ContextMenu("Apply")]
    void ApplyEasingToGameObjects()
    {
        var random = new Random(1);
        var enumNames = Enum.GetNames(typeof(Easing.EasingType));
        var enumCount = enumNames.Length;
        for (int i = 0; i < gameObjects.Length && i < enumCount; i++)
        {
            SimpleScaleLerp comp;
            if (gameObjects[i].TryGetComponent<SimpleScaleLerp>(out comp))
            {
                comp.easingType = (Easing.EasingType)i;
            }
            else
            {
                comp = gameObjects[i].AddComponent<SimpleScaleLerp>();
                comp.easingType = (Easing.EasingType)i;
            }

            gameObjects[i].transform.localPosition = new Vector3(0f, 0f, i * 4);
            gameObjects[i].name = enumNames[i];

            gameObjects[i].GetComponent<MeshRenderer>().material.color = new Color(random.NextFloat(0, 1), random.NextFloat(0, 1), random.NextFloat(0, 1));
        }
    }
}
