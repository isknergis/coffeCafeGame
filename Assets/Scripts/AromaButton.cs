using UnityEngine;
using UnityEngine.UI;

public class AromaButton : MonoBehaviour
{
    public string aromaName;

    public Button button;
    public Image image;

    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.white;
    public Color selectedColor = Color.green;

    public AromaSelection aromaSelection;

    bool isLocked = true;
    bool isSelected = false;

    void Start()
    {
      
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (isLocked) return;

        isSelected = !isSelected;

        if (isSelected)
        {
            image.color = selectedColor;
            aromaSelection.ToggleAroma(aromaName);
        }
        else
        {
            image.color = unlockedColor;
            aromaSelection.ToggleAroma(aromaName);
        }
    }

    public void Unlock()
    {
        isLocked = false;
        button.interactable = true;
        image.color = unlockedColor;
    }

    public void Lock()
    {
        isLocked = true;
        button.interactable = false;
        image.color = lockedColor;
    }

    public void ResetButton()
    {
        isSelected = false;
        if (!isLocked)
            image.color = unlockedColor;
    }
}