using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ServiceLocator))]
public class ServiceLocatorEditor : Editor
{
    [SerializeField]
    public Dictionary<Type, object> values;

    [SerializeField]
    public FieldInfo _dictField;

    [SerializeField]
    public List<Component> services = new List<Component>();

    [SerializeField]
    [Min(0)]
    public int size = 0;
    [SerializeField]
    public int oldSize = 0;
    [SerializeField]
    public bool openList;

    private void OnEnable()
    {
        values = new Dictionary<Type, object>();
        _dictField = typeof(ServiceLocator).GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        SerializedProperty asd = serializedObject.FindProperty("_services");
        Debug.Log(asd.type);
    }

    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
            return;

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Clear dict"))
        {
            values.Clear();
            _dictField.SetValue(target as ServiceLocator, values);
            size = 0;
            oldSize = 0;
            return;
        }
        if (GUILayout.Button("Print dict"))
        {
            foreach (Type type in ((Dictionary<Type, object>)_dictField.GetValue(target)).Keys)
                Debug.Log(type);
        }

        while (services.Count < size)
        {
            services.Add(null);
        }
        while (services.Count > size)
        {
            services.RemoveAt(services.Count - 1);
        }

        openList = EditorGUILayout.BeginFoldoutHeaderGroup(openList, new GUIContent(_dictField.Name));
        if (openList)
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Size");
            size = EditorGUILayout.DelayedIntField(size);

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel = 2;

            for (int i = 0; i < oldSize; i++)
            {
                Component temp = (Component)EditorGUILayout.ObjectField("Element" + i, (Component)services[i], typeof(Component), true);
                for (int j = 0; j < size; ++j)
                {
                    if (services[j] == null || temp == null)
                        continue;

                    if (services[j].GetType() == temp.GetType() && j != i)
                        throw new Exception("Tipo ya incluido");
                }
                services[i] = temp;
            }
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        oldSize = size;
       
        if (GUI.changed && EditorGUI.EndChangeCheck() && !Application.isPlaying)
        {
            Debug.Log("dirty bitch");
            MoveToDict();
            EditorUtility.SetDirty(target as ServiceLocator);
            EditorUtility.SetDirty(this);
            EditorSceneManager.MarkSceneDirty((target as ServiceLocator).gameObject.scene);
        }
    }

    public void MoveToDict()
    {
        values.Clear();

        for (int i = 0; i < size; ++i)
        {
            if (services[i] == null)
                continue;

            values.Add(services[i].GetType(), services[i]);
            Debug.Log(services[i].GetType());
        }
        _dictField.SetValue(target, new Dictionary<Type, object>(values));
    }
}
