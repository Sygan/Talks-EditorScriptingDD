using UnityEngine;

public class IMGUIExample : MonoBehaviour
{
    private bool _wasPressed = false;

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 200, 100), "IMGUI Example");

        if (GUI.Button(new Rect(20, 40, 180, 20), "Press me!"))
            _wasPressed = true;

        GUI.Label(new Rect(20, 70, 180, 40), "Was the button pressed?\n" +
            (_wasPressed ? "yes" : "no"));
    }
}