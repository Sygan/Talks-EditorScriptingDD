using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIElementsExample : EditorWindow
{
    [MenuItem("Editor Scripting!/UIElements/UIElementsExample")]
    public static void ShowExample()
    {
        UIElementsExample wnd = GetWindow<UIElementsExample>();
        wnd.titleContent = new GUIContent("UIElementsExample");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import Files
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/TalkSource/2 UIElements/Editor/UIElementsExample.uxml");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/TalkSource/2 UIElements/Editor/UIElementsExample.uss");

        //Create Visual Element Tree
        VisualElement uxmlLayout = visualTree.CloneTree();
        uxmlLayout.styleSheets.Add(styleSheet);
        root.Add(uxmlLayout);

        var pressButton = uxmlLayout.Q<Button>("PressButton");
        pressButton.clickable.clicked += OnPressButtonClicked;
        
    }

    private void OnPressButtonClicked()
    {
        Debug.Log("I was pressed!");
    }
}