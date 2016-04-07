using UnityEngine;

public class ModalPanel : MonoBehaviour {

    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;


    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }
        modalPanel.DisactivatePanel();
        return modalPanel;
    }

    public void ActivatePanel()
    {
        modalPanelObject.SetActive(true);
    }

    void DisactivatePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
