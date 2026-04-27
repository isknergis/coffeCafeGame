using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public OrderManager orderManager;
    public PlayerSelection player;
    public AromaSelection aroma;
    public UnlockSystem unlockSystem;

    public List<AromaButton> aromaButtons;

    public int money = 0;

    void Start()
    {
        unlockSystem.unlockedAromas = new List<string>();

        unlockSystem.unlockedAromas.Add("Vanilya");
        unlockSystem.unlockedAromas.Add("Kakao");

        unlockSystem.ApplyUnlocks();

        orderManager.unlockedAromas = unlockSystem.unlockedAromas;

        NewOrder();
    }

    public void OnServeButton()
    {
        bool correct = CheckOrder();

        if (correct)
        {
            money += 10;
            orderManager.playerLevel++;

            unlockSystem.CheckUnlock(orderManager.playerLevel);

            Debug.Log("DOÐRU");
        }
        else
        {
            money -= 5;
            Debug.Log("YANLIÞ");
        }

        Debug.Log("Para: " + money);

        ResetSelections();

        NewOrder();
    }

    void NewOrder()
    {
        orderManager.unlockedAromas = unlockSystem.unlockedAromas;
        orderManager.GenerateOrder();
    }

    void ResetSelections()
    {
        player.ResetSelection();
        aroma.ResetAromas();

        foreach (var btn in aromaButtons)
            btn.ResetButton();
    }

    bool CheckOrder()
    {
        var order = orderManager.currentOrder;

        if (order.coffeeType != player.selectedCoffee)
            return false;

        if (order.sugarLevel != player.sugarLevel)
            return false;

        if (order.aroma != null)
        {
            if (!aroma.selectedAromas.Contains(order.aroma))
                return false;
        }
        else
        {
            if (aroma.selectedAromas.Count > 0)
                return false;
        }

        return true;
    }
}