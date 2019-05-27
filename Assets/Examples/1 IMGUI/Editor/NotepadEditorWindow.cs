using UnityEngine;
using UnityEditor;

public class NotepadEditorWindow : EditorWindow
{
    private SerializedProperty _currentNoteProperty;
    private SerializedProperty _currentTextProperty;

    private SerializedObject _editorData;

    private NotepadEditorData _notepadEditorData;
    
    [MenuItem("Notepad/IMGUI Notepad")]
    public static void OpenWindow()
    {
        var window = GetWindow<NotepadEditorWindow>();
        window.name = "IMGUI Notepad";
        window.Show();
    }

    private void OnEnable()
    {
        _notepadEditorData = NotepadEditorData.Instance;

        _editorData = new SerializedObject(_notepadEditorData);

        _currentNoteProperty = _editorData.FindProperty("_currentNote");
        _currentTextProperty = _editorData.FindProperty("_currentText");

        if (_currentNoteProperty.objectReferenceValue != null)
        {
            _editorData.Update();

            var note = new SerializedObject(_currentNoteProperty.objectReferenceValue);

            _currentTextProperty.stringValue = note.FindProperty("_note").stringValue;

            _editorData.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    private void OnGUI()
    {
        var title = _currentNoteProperty.objectReferenceValue ? _currentNoteProperty.objectReferenceValue.name : "New Note";

        EditorGUILayout.BeginHorizontal("HelpBox", GUILayout.MaxWidth(200));

        EditorGUILayout.LabelField(title);

        if (GUILayout.Button("Save"))
            _notepadEditorData.Save();

        if (GUILayout.Button("Save As..."))
        {
            _notepadEditorData.SaveAs();
        }

        if (GUILayout.Button("Open"))
        {
            _notepadEditorData.Open();
        }

        EditorGUILayout.EndHorizontal();

        var rect = EditorGUILayout.GetControlRect();

        var textEditorRect = new Rect(rect.x, rect.y, position.width - rect.x - 2, position.height - 40);

        _editorData.Update();

        EditorGUI.PropertyField(textEditorRect, _currentTextProperty, GUIContent.none, false);

        _editorData.ApplyModifiedProperties();
    }
}