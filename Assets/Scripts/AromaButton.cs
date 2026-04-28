using UnityEngine;
using UnityEngine.UI;

public class AromaButton : MonoBehaviour
{
    public string aromaName;
    public Image image;

    public Color normalColor;
    public Color selectedColor;

    public AromaSelection aromaSelection;

    bool isSelected = false;

    public void OnClick()
    {
        aromaSelection.ToggleAroma(aromaName);

        isSelected = !isSelected;
        image.color = isSelected ? selectedColor : normalColor;
    }

    public void ResetButton()
    {
        isSelected = false;
        image.color = normalColor;
    }
}