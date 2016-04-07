using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIScript : MonoBehaviour
{
    public void PlayAgainButtonClick()
    {
        SceneManager.LoadScene("titleScene");
    }
}
