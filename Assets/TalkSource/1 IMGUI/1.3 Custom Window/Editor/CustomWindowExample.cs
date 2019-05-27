using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class CustomWindowExample : EditorWindow
{
    private List<ScriptableObjectExample> _foundAssets = new List<ScriptableObjectExample>();
    private Vector2 _scrollPosition;

    [MenuItem("Editor Scripting!/Custom Window Example")]
    public static void OpenWindow()
    {
        var window = GetWindow<CustomWindowExample>("Custom Window");

        window.position = new Rect(100, 100, 300, 600);

        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("This window finds all Example Scriptable Objects");

        if(GUILayout.Button("Find Assets!"))
            FindAssets();

        if (GUILayout.Button("Fix Assets!"))
            FixAssets();

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        EditorGUI.BeginDisabledGroup(true);

        if (_foundAssets == null || _foundAssets.Count == 0)
        {
            EditorGUILayout.LabelField("No Assets Found! Have you pressed \"Find Assets\" button?");
        }
        else
        {
            foreach (var asset in _foundAssets)
                EditorGUILayout.ObjectField(asset, typeof(ScriptableObjectExample), false);
        }   

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndScrollView();
    }

    private void FindAssets()
    {
        if (_foundAssets == null)
            _foundAssets = new List<ScriptableObjectExample>();
        else
            _foundAssets.Clear();

        var assetGuids = AssetDatabase.FindAssets("t:ScriptableObjectExample");

        if (assetGuids == null || assetGuids.Length == 0)
            return;

        foreach(var assetGuid in assetGuids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

            if (string.IsNullOrEmpty(assetPath))
                continue;

            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObjectExample>(assetPath);

            if (asset != null)
                _foundAssets.Add(asset);
        }
    }

    private void FixAssets()
    {
        if (!EditorUtility.DisplayDialog("Are you sure?", "Are you sure? This operation can't be undone.", "Yes", "Cancel"))
            return;
        
        FindAssets();

        foreach(var asset in _foundAssets)
        {
            var serializedObject = new SerializedObject(asset);

            serializedObject.Update();

            var firstValue = serializedObject.FindProperty("_firstValue");
            var secondValue = serializedObject.FindProperty("_secondValue");

            if (firstValue.intValue > secondValue.intValue)
            {
                firstValue.intValue = secondValue.intValue;
                Debug.LogFormat(asset, "Fixed object: {0}.", asset.name);
            }

            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}