using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageElement : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private Sprite arabicSprite;
    [SerializeField]
    private Sprite englishSprite;

    void Awake()
    {

    }
    public void Init()
    {
        image = GetComponent<Image>();
    }
    public void SetLanguage(string language)
    {
        Sprite currentSprite = null;
        switch (language)
        {
            case "English":
                currentSprite = englishSprite;
                break;

            case "Arabic":
                currentSprite = arabicSprite;
                break;
        }
        image.sprite = currentSprite;
    }
}
