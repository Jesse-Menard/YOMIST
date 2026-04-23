using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuColorControl : MonoBehaviour
{
    [SerializeField]
    public List<Image> ImagesToColorChange = new List<Image>();
    [SerializeField]
    public List<TMP_Text> TextToColorChange = new List<TMP_Text>();

    [SerializeField]
    public Color MenuUIColor;
    [SerializeField]
    public Color TextUIColor;

    void OnValidate()
    {
        ChangeColors();
    }

    void ChangeColors()
    {
        foreach (var image in ImagesToColorChange)
        {
            image.color = MenuUIColor;            
        }

        foreach (var text in TextToColorChange)
        {
            text.color = TextUIColor;
        }
    }
}
