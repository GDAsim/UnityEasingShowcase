using System;
using UnityEngine;

public class ScaleObjects : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;

    void Awake()
    {
        ApplyEasingToGameObjects();
    }

    [ContextMenu("Apply")]
    void ApplyEasingToGameObjects()
    {
        var enumNames = Enum.GetNames(typeof(EasingFunctions.EasingType));
        var enumCount = enumNames.Length;
        for (int i = 0; i < gameObjects.Length && i < enumCount; i++)
        {
            SimpleScaleLerp comp;
            if (gameObjects[i].TryGetComponent<SimpleScaleLerp>(out comp))
            {
                comp.easingType = (EasingFunctions.EasingType)i;
            }
            else
            {
                comp = gameObjects[i].AddComponent<SimpleScaleLerp>();
                comp.easingType = (EasingFunctions.EasingType)i;
            }

            gameObjects[i].transform.localPosition = new Vector3(0f, 0f, i * 4);
            gameObjects[i].name = enumNames[i];
        }
    }
}
