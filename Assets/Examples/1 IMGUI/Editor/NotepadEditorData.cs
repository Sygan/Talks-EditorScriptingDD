using UnityEngine;
using UnityEditor;

public class NotepadEditorData : ScriptableObject
{
    private static NotepadEditorData _instance;

    public static NotepadEditorData Instance
    {
        get
        {
            if (_instance == null)
            {
                var assets = AssetDatabase.FindAssets("t:NotepadEditorData");

                if (assets != null && assets.Length > 0)
                {
                    var guid = assets[0];
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    
                    _instance = AssetDatabase.LoadAssetAtPath<NotepadEditorData>(path);
                }
            }

            if (_instance == null)
            {
                _instance = CreateInstance<NotepadEditorData>();

                AssetDatabase.CreateAsset(_instance, "Assets/NotepadEditorData.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            return _instance;
        }
    }

    [SerializeField]
    private NoteData _currentNote;

    public NoteData CurrentNote => _currentNote;

    [SerializeField]
    [TextArea]
    private string _currentText;

    public void Save()
    {
        var note = new SerializedObject(_currentNote);

        if (_currentNote != null)
        {
            note.Update();

            note.FindProperty("_note").stringValue = _currentText;

            note.ApplyModifiedPropertiesWithoutUndo();
        }
    }

    public void SaveAs()
    {
        var path = EditorUtility.SaveFilePanelInProject("Save As...", "New Note", "asset", "Save note as...");

        if (string.IsNullOrEmpty(path))
            return;

        var curAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

        SerializedObject note = null;

        if (curAsset != null)
        {
            if (curAsset is NoteData)
                note = new SerializedObject(curAsset);
            else
            {
                Debug.LogError("There is already an asset at this path!");
                return;
            }
        }
        else
        {
            var noteInstance = CreateInstance<NoteData>();

            note = new SerializedObject(noteInstance);

            AssetDatabase.CreateAsset(noteInstance, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        note.Update();

        note.FindProperty("_note").stringValue = _currentText;

        note.ApplyModifiedPropertiesWithoutUndo();

        SetCurrentNote(note.targetObject as NoteData);
    }

    public void Open()
    {
        var path = EditorUtility.OpenFilePanel("Open", Application.dataPath, "asset");

        if (string.IsNullOrEmpty(path))
            return;

        var assetPath = path.Remove(0, Application.dataPath.Length - "Assets".Length);
        var curAsset = AssetDatabase.LoadAssetAtPath<NoteData>(assetPath);

        if (curAsset == null)
        {
            Debug.LogError("There is no valid asset at this path!");
            return;
        }

        SetCurrentNote(curAsset as NoteData);
    }

    private void SetCurrentNote(NoteData note)
    {
        var editorData = new SerializedObject(this);

        editorData.Update();

        editorData.FindProperty(nameof(_currentNote)).objectReferenceValue = note;

        editorData.FindProperty(nameof(_currentText)).stringValue = note.Note;

        editorData.ApplyModifiedPropertiesWithoutUndo();
    }
}