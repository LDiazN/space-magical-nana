using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    SerializedProperty mask;

    private void OnEnable()
    {
        mask = serializedObject.FindProperty("_targetLayer");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        mask.intValue = EditorGUILayout.MaskField(mask.intValue, LayersNames());

        if (!IsPowerOfTwo(mask.intValue))
            return;
        else if (mask.intValue != 0)
            Debug.Log(GetRealLayer(CalculateFakeIndex(mask.intValue), LayersNames()));

        serializedObject.ApplyModifiedProperties();
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
