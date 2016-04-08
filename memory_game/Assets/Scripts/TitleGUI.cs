using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGUI : MonoBehaviour
{

    public GUISkin customSkin;

    void OnGUI()
    {
        int buttonWidth = 100;
        int buttonHeight = 50;
        float halfScreenW = Screen.width * 0.5f;
        float halfButtonW = buttonWidth * 0.5f;
        GUI.skin = customSkin;    
    }


    public void LoadScene()
    {
            SceneManager.LoadScene("gameScene");
     
    }
}
