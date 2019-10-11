using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TouchScreenController TouchScreenController;
    [SerializeField]
    private GameObject[] uiPanels;
    private int activePanelIndex = -1;

    void Start()
    {

    }
    public void Init(TouchScreenController TouchScreenController)
    {
        this.TouchScreenController=TouchScreenController;
        activePanelIndex = -1;
        ResetPanles();
    }
    public void ChoosePanel(int panelIndex)
    {
        if (activePanelIndex != -1)
        {
            uiPanels[activePanelIndex].SetActive(false);
        }
        for (int i = 0; i < uiPanels.Length; i++)
        {
            if ((panelIndex == i) && (!uiPanels[i].activeSelf))
            {
                uiPanels[i].SetActive(true);
                activePanelIndex = i;

                break;
            }
        }
    }
    public void SendMessageToMonitors(string message)
    {
        TouchScreenController.SendMessageToMonitors(message);
    }
    public void ChooseLogo(string logoName)
    {
        TouchScreenController.SetLogo(logoName);
    }
    public void ResetLogo()
    {
        TouchScreenController.ResetLogo();
    }
    private void ResetPanles()
    {
        for (int i = 0; i < uiPanels.Length; i++)
        {
            if (uiPanels[i].activeSelf)
                uiPanels[i].SetActive(false);
        }
    }
}
