using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenController : MonoBehaviour
{
    [SerializeField]
    private UIManager UIManager;
    [SerializeField]
    private LanguageManager LanguageManager;
    [SerializeField]
    private TouchAndThrowController TouchAndThrowController;

    [SerializeField]
    private string selectedLogo;
    [SerializeField]
    private GameObject TharsheedLogo, AbhuDhabiLogo;

    public string SelectedLogo { get => selectedLogo; }

    void Start()
    {
        Init();
    }
    private void Init()
    {
        Application.targetFrameRate = 60;

        TouchAndThrowController.Init(this);
        UIManager.Init(this);
        UIManager.ChoosePanel(0);
        LanguageManager.Init();
        SetLanguage("English");

        ResetLogo();
    }
    public void SendMessageToMonitors(string message)
    {
        //send message here via tcp
        Debug.Log("sending message= " + message);
    }
    public void SetLogo(string logoName)
    {
        selectedLogo = logoName;

        TharsheedLogo.SetActive(false);
        AbhuDhabiLogo.SetActive(false);

        if (logoName == "Tharsheed")
        {
            TharsheedLogo.SetActive(true);
        }
        else
        {
            AbhuDhabiLogo.SetActive(true);
        }
    }
    public void ResetLogo()
    {
        selectedLogo = "";
        TharsheedLogo.SetActive(false);
        AbhuDhabiLogo.SetActive(false);
        TouchAndThrowController.Reset();
    }
    public void SetLanguage(string language)
    {
        LanguageManager.SetLanguage(language);
    }
}
