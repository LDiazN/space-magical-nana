using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UpgradeableShip))]
public class ShipEditor : Editor
{
    SerializedProperty fromGarage;
    SerializedProperty garageShip;
    SerializedProperty baseStats;
    SerializedProperty upgrades;

    private void OnEnable()
    {
        fromGarage = serializedObject.FindProperty("_loadFromGarage");
        garageShip = serializedObject.FindProperty("_garageShip");
        baseStats = serializedObject.FindProperty("_baseStats");
        upgrades = serializedObject.FindProperty("_upgrades");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(fromGarage, new GUIContent("Garage Load"));
        if (fromGarage.boolValue)
        {
            EditorGUILayout.PropertyField(garageShip, new GUIContent("Player Ship"));
        }
        else
        {
            EditorGUILayout.PropertyField(baseStats, new GUIContent("Base Stats"));
            EditorGUILayout.PropertyField(upgrades);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
