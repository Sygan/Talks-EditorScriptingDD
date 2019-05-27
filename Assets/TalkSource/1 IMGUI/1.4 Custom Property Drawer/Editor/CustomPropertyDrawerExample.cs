using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CustomProperty))]
public class CustomPropertyDrawerExample : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var actualRect = EditorGUI.PrefixLabel(position, label);

        EditorGUI.PropertyField(
            new Rect(actualRect.x, actualRect.y, actualRect.width / 2 - 5, actualRect.height), 
            property.FindPropertyRelative("ID"), GUIContent.none);

        EditorGUI.PropertyField(new Rect(actualRect.x + actualRect.width / 2 + 5,
            actualRect.y, actualRect.width / 2 - 10, actualRect.height),
            property.FindPropertyRelative("ExampleObject"), GUIContent.none);
    }
}