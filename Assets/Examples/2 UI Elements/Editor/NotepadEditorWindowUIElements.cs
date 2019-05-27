using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class NotepadEditorWindowUIElements : EditorWindow
{
    private NotepadEditorData _notepadEditorData;

    private Label _titleLabel;

    [MenuItem("Notepad/NotepadEditorWindowUIElements")]
    public static void ShowExample()
    {
        NotepadEditorWindowUIElements wnd = GetWindow<NotepadEditorWindowUIElements>();
        wnd.titleContent = new GUIContent("NotepadEditorWindowUIElements");
    }

    public void OnEnable()
    {
        _notepadEditorData = NotepadEditorData.Instance;

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Examples/2 UI Elements/Editor/NotepadEditorWindowUIElements.uss");
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Examples/2 UI Elements/Editor/NotepadEditorWindowUIElements.uxml");
        VisualElement uxmlLayout = visualTree.CloneTree();
        uxmlLayout.styleSheets.Add(styleSheet);
        root.Add(uxmlLayout);

        var imgui = uxmlLayout.Q<IMGUIContainer>("NoteArea");
        imgui.onGUIHandler += DrawNoteArea;

        _titleLabel = uxmlLayout.Q<Label>("FileName");
        SetTitleLabel();

        var saveButton = uxmlLayout.Q<Button>("SaveButton");
        saveButton.clickable.clicked += _notepadEditorData.Save;

        var saveAsButton = uxmlLayout.Q<Button>("SaveAsButton");
        saveAsButton.clickable.clicked += _notepadEditorData.SaveAs;
        saveAsButton.clickable.clicked += SetTitleLabel;

        var openButton = uxmlLayout.Q<Button>("OpenButton");
        openButton.clickable.clicked += _notepadEditorData.Open;
        openButton.clickable.clicked += SetTitleLabel;
    }

    private void SetTitleLabel()
    {
        _titleLabel.text = _notepadEditorData.CurrentNote ? _notepadEditorData.CurrentNote.name : "New Note";
    }

    private void DrawNoteArea()
    {
        var editorData = new SerializedObject(_notepadEditorData);

        var rect = EditorGUILayout.GetControlRect();

        var textEditorRect = new Rect(rect.x, rect.y, position.width - rect.x - 2, position.height - 40);

        editorData.Update();

        EditorGUI.PropertyField(textEditorRect, editorData.FindProperty("_currentText"), GUIContent.none, false);

        editorData.ApplyModifiedProperties();
    }
}