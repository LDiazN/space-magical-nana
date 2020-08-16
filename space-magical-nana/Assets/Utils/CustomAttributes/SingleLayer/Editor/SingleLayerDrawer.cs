using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(SingleLayerAttribute))]
public class SingleLayerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int oldValue = property.intValue;
        EditorGUI.BeginProperty(position, label, property);

        property.intValue = EditorGUI.MaskField(position, label, property.intValue, LayersNames());

        if (!IsPowerOfTwo(property.intValue))
        {
            property.intValue = oldValue;
        }
        else if (property.intValue != 0)
        {
            oldValue = property.intValue;
            Debug.Log(GetRealLayer(CalculateFakeIndex(property.intValue), LayersNames()));
        }

        EditorGUI.EndProperty();
    }

    public bool IsPowerOfTwo(int number)
    {
        return (number & (number - 1)) == 0;
    }

    public int CalculateFakeIndex(int value)
    {
        for (int i = 0; i < LayersNames().Length; ++i)
            if (Mathf.Pow(2, i) == value) return i;

        throw new System.Exception("No deberia pasar");
    }

    public string[] LayersNames()
    {
        List<string> layerList = new List<string>();
        for (int i = 0; i < 32; ++i)
        {
            if (LayerMask.LayerToName(i) != "")
                layerList.Add(LayerMask.LayerToName(i));
        }
        return layerList.ToArray();
    }

    public string[] RealLayers()
    {
        string[] layers = new string[32];
        for (int i = 0; i < 32; ++i)
            layers[i] = LayerMask.LayerToName(i);

        return layers;
    }

    public int GetRealLayer(int fakeLayer, string[] fakeLayers)
    {
        int real = 0;
        string[] realLayers = RealLayers();

        for (real = 0; real < 32 && fakeLayers[fakeLayer] != realLayers[real]; ++real)
        { }

        return real;
    }
}
