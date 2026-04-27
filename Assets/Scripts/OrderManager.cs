using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public List<string> coffeeTypes;
    public List<string> unlockedAromas;

    public int playerLevel = 1;
    public CoffeeOrder currentOrder;

    public Text orderText;

    public CoffeeOrder GenerateOrder()
    {
        CoffeeOrder order = new CoffeeOrder();

        order.coffeeType = coffeeTypes[
            Random.Range(0, coffeeTypes.Count)
        ];

        order.sugarLevel = (playerLevel < 3)
            ? Random.Range(0, 2)
            : Random.Range(0, 3);

        if (unlockedAromas.Count > 0 && Random.value > 0.5f)
            order.aroma = unlockedAromas[
                Random.Range(0, unlockedAromas.Count)
            ];
        else
            order.aroma = null;

        currentOrder = order;

        string sugarText = "Sade";
        if (order.sugarLevel == 1) sugarText = "Orta";
        if (order.sugarLevel == 2) sugarText = "Çok";

        if (orderText != null)
        {
            orderText.text =
                order.coffeeType +
                "\nÞeker: " + sugarText +
                "\nAroma: " + (order.aroma ?? "Yok");
        }

        Debug.Log("Sipariþ: " + order.coffeeType +
                  " | Þeker: " + sugarText +
                  " | Aroma: " + order.aroma);

        return order;
    }
}