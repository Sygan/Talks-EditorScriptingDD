using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Notepad/Create New Note")]
public class NoteData : ScriptableObject
{
    [SerializeField]
    [TextArea(10, 1000)]
    private string _note;

    public string Note => _note;
}