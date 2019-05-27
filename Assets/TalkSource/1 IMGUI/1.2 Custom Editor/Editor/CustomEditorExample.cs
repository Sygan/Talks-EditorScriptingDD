using UnityEditor;

[CustomEditor(typeof(ScriptableObjectExample))]
public class CustomEditorExample : Editor
{

    private void OnEnable()
    {
        //Direct Method
        //DirectGUIOnEnable();

        //Property Method
        PropertyGUIOnEnable();
    }

    public override void OnInspectorGUI()
    {
        //Direct Method
        //DoDirectGUI();

        //Property Method
        //DoPropertyGUI();

        DoPrettyVersion();
    }

    #region Direct Method

    private ScriptableObjectExample _scriptableObjectExample;

    private void DirectGUIOnEnable()
    {
        _scriptableObjectExample = target as ScriptableObjectExample;
    }

    private void DoDirectGUI()
    {
        EditorGUILayout.LabelField("Name: " + _scriptableObjectExample.name, EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();

        _scriptableObjectExample.FirstValue = EditorGUILayout.IntField("FirstValue",
            _scriptableObjectExample.FirstValue);
        _scriptableObjectExample.SecondValue = EditorGUILayout.IntField("SecondValue",
            _scriptableObjectExample.SecondValue);

        EditorGUILayout.LabelField("FirstValue cannot be higher than SecondValue!");

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(_scriptableObjectExample);
    }

    #endregion

    #region Property Method

    private SerializedProperty _firstValueProperty;
    private SerializedProperty _secondValueProperty;

    private void PropertyGUIOnEnable()
    {
        _firstValueProperty = serializedObject.FindProperty("_firstValue");
        _secondValueProperty = serializedObject.FindProperty("_secondValue");
    }

    private void DoPropertyGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Name: " + target.name, EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(_firstValueProperty);
        EditorGUILayout.PropertyField(_secondValueProperty);

        EditorGUILayout.LabelField("FirstValue cannot be higher than SecondValue!");

        serializedObject.ApplyModifiedProperties();
    }

    private void DoPrettyVersion()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Name: " + target.name, EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_firstValueProperty);
        EditorGUILayout.PropertyField(_secondValueProperty);

        var ok = _firstValueProperty.intValue <= _secondValueProperty.intValue;

        EditorGUILayout.HelpBox("FirstValue cannot be higher than SecondValue.", ok ? MessageType.Info : MessageType.Error);

        serializedObject.ApplyModifiedProperties();
    }


    #endregion
}