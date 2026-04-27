using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public string selectedCoffee;
    public int sugarLevel; // 0-2

    public void SelectCoffee(string coffee)
    {
        selectedCoffee = coffee;
        Debug.Log("Seçilen kahve: " + coffee);
    }

    public void SetSugar(int level)
    {
        sugarLevel = level;
        Debug.Log("Þeker: " + level);
    }

    public void ResetSelection()
    {
        selectedCoffee = null;
        sugarLevel = 0;
    }
}