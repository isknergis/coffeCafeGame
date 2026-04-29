using UnityEngine;
using UnityEngine.UI;

public class AromaButton : MonoBehaviour
{
    public string aromaName;

    public Image buttonImage;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    AromaSelection aromaSelection;
    GameManager gameManager;

    bool isSelected = false;

    void Start()
    {
        aromaSelection = FindFirstObjectByType<AromaSelection>();
        gameManager = FindFirstObjectByType<GameManager>();

        ResetButton();
    }

    public void OnClick()
    {
        if (gameManager != null && !gameManager.CanSelect())
            return;

        isSelected = !isSelected;

        aromaSelection.ToggleAroma(aromaName);

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (buttonImage != null)
            buttonImage.color = isSelected ? selectedColor : normalColor;
    }

    public void ResetButton()
    {
        isSelected = false;

        if (buttonImage != null)
            buttonImage.color = normalColor;
    }
}