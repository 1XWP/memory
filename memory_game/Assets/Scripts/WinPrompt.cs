using UnityEngine;
using UnityEngine.UI;

public class WinPrompt : MonoBehaviour
{

    public GameObject winPromptObject;
    public GameObject timeText;

    private static WinPrompt winPrompt;

    public static WinPrompt Instance()
    {
        if (!winPrompt)
        {
            winPrompt = FindObjectOfType(typeof(WinPrompt)) as WinPrompt;
            if (!winPrompt)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }
        return winPrompt;
    }
}
