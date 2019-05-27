using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AttributesExample : MonoBehaviour
{
    public int Variable = 1;

    [HideInInspector]
    public int SomeVariable = 2;

    [SerializeField]
    [Range(0, 5)]
    private int _someOtherVariable = 3;

    [SerializeField]
    [Multiline(10)]
    private string _textAreaVariable = "Four";

    [SerializeField]
    private SerializedClass _serializedClassVariable;

#if UNITY_EDITOR

    [MenuItem("Editor Scripting!/Click Me!")]
    public static void CustomMenuEntry()
    {
        Debug.Log("I am invoked from custom menu entry!");
    }

#endif

}

[Serializable]
public class SerializedClass
{
    [SerializeField]
    private int _intVariableInClass = 5; 

    [SerializeField]
    private bool _boolVariableInClass = false;
}
