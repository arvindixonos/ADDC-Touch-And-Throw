using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    private string currentLanguage;
    [SerializeField]
    private LanguageElement[] languageElements;

    void Start()
    {

    }
    public void Init()
    {
        for (int i = 0; i < languageElements.Length; i++)
        {
            languageElements[i].Init();
        }
    }
    public void SetLanguage(string languageToSet)
    {
        currentLanguage = languageToSet;

        for (int i = 0; i < languageElements.Length; i++)
        {
            languageElements[i].SetLanguage(currentLanguage);
        }
    }


}
