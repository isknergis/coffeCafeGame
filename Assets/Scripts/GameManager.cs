using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public OrderManager orderManager;
    public PlayerSelection player;
    public AromaSelection aroma;
    public UnlockSystem unlockSystem;
    public CoffeeMachine machine;

    [Header("UI")]
    public Button serveButton;
    public Text moneyText;

    [Header("Buttons")]
    public List<AromaButton> aromaButtons;

    [Header("Economy")]
    public List<CoffeeData> coffeePrices;

    [System.Serializable]
    public class AromaData
    {
        public string aromaName;
        public int price;
    }

    public List<AromaData> aromaPrices;

    int money = 0;

    // ---------------- START ----------------
    void Start()
    {
        unlockSystem.unlockedAromas = new List<string>();
        unlockSystem.unlockedAromas.Add("Vanilya");
        unlockSystem.unlockedAromas.Add("Kakao");

        orderManager.unlockedAromas = unlockSystem.unlockedAromas;
        orderManager.GenerateOrder();

        serveButton.interactable = false;
        machine.OnCoffeeReady += EnableServeButton;

        UpdateMoneyUI();
    }

    void EnableServeButton()
    {
        serveButton.interactable = true;
    }

    public bool CanSelect()
    {
        return !machine.IsBrewing();
    }

    // ---------------- SERVE ----------------
    public void OnServeButton()
    {
        serveButton.interactable = false;

        bool correct = CheckOrder();

        if (correct)
        {
            int earned = CalculateEarnings(orderManager.currentOrder.coffeeType);
            money += earned;

            Debug.Log("DOÐRU +" + earned);

            // ?? level kontrol (çok hýzlý artmasýn)
            if (money > 100) orderManager.playerLevel = 2;
            if (money > 300) orderManager.playerLevel = 3;
        }
        else
        {
            money -= 5;
            Debug.Log("YANLIÞ");
        }

        Debug.Log("Para: " + money);

        ResetAll();
        orderManager.GenerateOrder();
        machine.ResetMachine();

        UpdateMoneyUI();
    }

    // ---------------- RESET ----------------
    void ResetAll()
    {
        player.ResetSelection();
        aroma.ResetAromas();

        foreach (var btn in aromaButtons)
            btn.ResetButton();
    }

    // ---------------- CHECK ----------------
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

        if (!machine.IsCoffeeReady())
        {
            Debug.Log("Kahve hazýr deðil!");
            return false;
        }

        return true;
    }

    // ---------------- FAIL ----------------
    public void OnOrderFailed()
    {
        money -= 10;
        Debug.Log("Sipariþ kaçtý! -10");

        ResetAll();

        if (machine != null)
            machine.ResetMachine();

        UpdateMoneyUI();
    }

    // ---------------- PRICE ----------------
    int GetCoffeePrice(string coffeeName)
    {
        foreach (var coffee in coffeePrices)
        {
            if (coffee.coffeeName == coffeeName)
                return coffee.basePrice;
        }

        return 10;
    }

    int GetAromaPrice(string aromaName)
    {
        foreach (var a in aromaPrices)
        {
            if (a.aromaName == aromaName)
                return a.price;
        }

        return 0;
    }

    // ?? TÜM KAZANÇ HESABI (DOÐRU HAL)
    int CalculateEarnings(string coffeeName)
    {
        int total = GetCoffeePrice(coffeeName);

        // aroma ekle
        if (orderManager.currentOrder.aroma != null)
        {
            int aromaPrice = GetAromaPrice(orderManager.currentOrder.aroma);
            total += aromaPrice;

            Debug.Log("Aroma fiyatý: " + aromaPrice);
        }

        // dengeli multiplier
        float multiplier = 1f + (orderManager.playerLevel * 0.05f);

        return Mathf.RoundToInt(total * multiplier);
    }

    // ---------------- UI ----------------
    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Para: " + money;
    }
}