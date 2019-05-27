using UnityEngine;

[CreateAssetMenu(menuName ="Editor Scripting!/Example Scriptable Object", 
    fileName = "New Scriptable Object")]
public class ScriptableObjectExample : ScriptableObject
{
    [SerializeField]
    [Tooltip("First Value needs to be smaller than second value.")]
    private int _firstValue;

    [SerializeField]
    [Tooltip("Second Value needs to be larger or equal to first value.")]
    private int _secondValue;

    public int FirstValue
    {
        get { return _firstValue; }
        set { _firstValue = value; }
    }

    public int SecondValue
    {
        get { return _secondValue; }
        set { _secondValue = value; }
    }

    public void OnValidate()
    {
        if (_firstValue > _secondValue)
            Debug.LogErrorFormat(this, "FirstValue cannot be higher than SecondValue in {0}." +
                "\n(The values are FirstValue: '{1}' and SecondValue: '{2}')", 
                name, _firstValue, _secondValue);
    }
}