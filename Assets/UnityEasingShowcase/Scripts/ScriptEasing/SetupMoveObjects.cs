using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SetupMoveObjects : MonoBehaviour
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
            SimpleMoveLerp comp;
            if (gameObjects[i].TryGetComponent<SimpleMoveLerp>(out comp))
            {
                comp.easingType = (Easing.EasingType)i;
            }
            else
            {
                comp = gameObjects[i].AddComponent<SimpleMoveLerp>();
                comp.easingType = (Easing.EasingType)i;
            }

            gameObjects[i].transform.localPosition = new Vector3(0f, 0f, i * 4);
            gameObjects[i].name = enumNames[i];

#if UNITY_EDITOR
            var mat = gameObjects[i].GetComponent<MeshRenderer>().material;
#else
            var mat = gameObjects[i].GetComponent<MeshRenderer>().sharedMaterial;
#endif

            mat.color = new Color(random.NextFloat(0, 1), random.NextFloat(0, 1), random.NextFloat(0, 1));
        }
    }
}
