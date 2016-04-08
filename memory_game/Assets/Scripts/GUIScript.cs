using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class providing methods for GUI buttons
/// </summary>
public class GUIScript : MonoBehaviour
{   
    /// <summary>
    /// Function loading scene again
    /// </summary>
    public void PlayAgainButtonClick()
    {
        SceneManager.LoadScene("titleScene");
    }

    /// <summary>
    /// Function exiting application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function loading scene
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("gameScene");
    }
}