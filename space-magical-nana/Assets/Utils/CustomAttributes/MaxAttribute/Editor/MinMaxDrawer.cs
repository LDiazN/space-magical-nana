using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MaxAttribute))]
public class MaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        MaxAttribute max = attribute as MaxAttribute;

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(position, property);

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = (property.intValue < max.Max ? property.intValue : (int)max.Max);
                break;
            case SerializedPropertyType.Float:
                property.floatValue = (property.floatValue < max.Max ? property.floatValue : max.Max);
                break;
        }

        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer(typeof(MinMaxAttribute))]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);

        MinMaxAttribute minMax = attribute as MinMaxAttribute;

        EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(position, property);

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = (property.intValue > minMax.Min ? property.intValue : (int)minMax.Min);
                property.intValue = (property.intValue < minMax.Max ? property.intValue : (int)minMax.Max);
                break;
            case SerializedPropertyType.Float:
                property.floatValue = (property.floatValue > minMax.Min ? property.floatValue : minMax.Min);
                property.floatValue = (property.floatValue < minMax.Max ? property.floatValue : minMax.Max);
                break;
        }

        EditorGUI.EndProperty();
    }
}