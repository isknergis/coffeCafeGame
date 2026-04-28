using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public string selectedCoffee;

    public int sugarLevel = 0; // 0-1-2

    [Header("UI")]
    public Text sugarText; // ekranda yazacak (opsiyonel)

    public void SelectCoffee(string coffee)
    {
        selectedCoffee = coffee;
        Debug.Log("Seçilen kahve: " + coffee);
    }

    // ?? TEK BUTON ÞEKER
    public void NextSugar()
    {
        sugarLevel++;

        if (sugarLevel > 2)
            sugarLevel = 0;

        UpdateSugarUI();
    }

    void UpdateSugarUI()
    {
        string text = "";

        if (sugarLevel == 0) text = "Sade";
        if (sugarLevel == 1) text = "Orta";
        if (sugarLevel == 2) text = "Çok";

        Debug.Log("Þeker: " + text);

        if (sugarText != null)
            sugarText.text = "Þeker: " + text;
    }

    public void ResetSelection()
    {
        selectedCoffee = null;
        sugarLevel = 0;
        UpdateSugarUI();
    }
}