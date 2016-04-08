using UnityEngine;

/// <summary>
/// Class providing methods for ModalPanel game object
/// </summary>
public class ModalPanel : MonoBehaviour {

    public GameObject modalPanelObject;

    public void ActivatePanel()
    {
        modalPanelObject.SetActive(true);
    }

    void DisactivatePanel()
    {
        modalPanelObject.SetActive(false);
    }
}