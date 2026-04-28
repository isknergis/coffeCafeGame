using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public List<string> coffeeTypes;
    public List<string> unlockedAromas;

    public int playerLevel = 1;

    public CoffeeOrder currentOrder;

    public Text orderText;

    public void GenerateOrder()
    {
        currentOrder = new CoffeeOrder();

        currentOrder.coffeeType = coffeeTypes[Random.Range(0, coffeeTypes.Count)];
        currentOrder.sugarLevel = Random.Range(0, 3);

        if (unlockedAromas.Count > 0)
            currentOrder.aroma = unlockedAromas[Random.Range(0, unlockedAromas.Count)];
        else
            currentOrder.aroma = null;

        UpdateUI();

        Debug.Log("Sipariþ: " + currentOrder.coffeeType +
                  " | Þeker: " + currentOrder.sugarLevel +
                  " | Aroma: " + currentOrder.aroma);
    }

    void UpdateUI()
    {
        if (orderText == null) return;

        string sugarText = currentOrder.sugarLevel == 0 ? "Sade" :
                           currentOrder.sugarLevel == 1 ? "Orta" : "Çok";

        orderText.text =
            currentOrder.coffeeType + "\n" +
            "Þeker: " + sugarText + "\n" +
            "Aroma: " + (currentOrder.aroma ?? "Yok");
    }
}