using UnityEngine;
using System.Collections;

public class TitleGUI : MonoBehaviour {

    public GUISkin customSkin;

    void OnGUI()
    {
        int buttonWidth = 100;
        int buttonHeight = 50;
        float halfScreenW = Screen.width * 0.5f;
        float halfButtonW = buttonWidth * 0.5f;
        GUI.skin = customSkin;
        if(GUI.Button (new Rect(halfScreenW-halfButtonW, 250, buttonWidth, buttonHeight), "Play"))
        {
            Application.LoadLevel("gameScene");
        }
    }
}
