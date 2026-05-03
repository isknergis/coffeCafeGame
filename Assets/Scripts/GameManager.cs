using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Button gunuBitirButton; // Inspector'dan butonu buraya sürükle

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

        ResetSelections();
        orderManager.GenerateOrder();

        serveButton.interactable = false;
        machine.OnCoffeeReady += EnableServeButton;

        UpdateMoneyUI();

        // --- Hatalý Kýsým Düzeltildi ---
        if (gunuBitirButton != null)
        {
            gunuBitirButton.onClick.AddListener(GunuBitir);
        }
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

            // Verileri Sahne Arasýna Kaydet
            GameData.GununKazanci += earned;
            GameData.ToplamSiparis++;

            Debug.Log("DOÐRU +" + earned);

            if (money > 100) orderManager.playerLevel = 2;
            if (money > 300) orderManager.playerLevel = 3;
        }
        else
        {
            money -= 5;
            GameData.IptalEdilenSiparis++;
            Debug.Log("YANLIÞ");
        }

        ResetSelections();
        orderManager.GenerateOrder();
        machine.ResetMachine();

        UpdateMoneyUI();
        unlockSystem.CheckUnlock(orderManager.playerLevel);
        orderManager.unlockedAromas = unlockSystem.unlockedAromas;
    }

    // ---------------- RESET ----------------
    public void ResetSelections()
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

        if (order.aromas != null && order.aromas.Count > 0)
        {
            foreach (var a in order.aromas)
            {
                if (!aroma.selectedAromas.Contains(a))
                    return false;
            }
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
        GameData.IptalEdilenSiparis++;
        Debug.Log("Sipariþ kaçtý! -10");

        ResetSelections();

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

    // ---------------- EARNINGS ----------------
    int CalculateEarnings(string coffeeName)
    {
        int total = GetCoffeePrice(coffeeName);

        if (orderManager.currentOrder.aromas != null)
        {
            foreach (var a in orderManager.currentOrder.aromas)
            {
                total += GetAromaPrice(a);
            }
        }

        float multiplier = 1f + (orderManager.playerLevel * 0.05f);
        return Mathf.RoundToInt(total * multiplier);
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Para: " + money;
    }

    // ---------------- SCENE NAVIGATION ----------------
    public void GunuBitir()
    {
        Debug.Log("Butona týklandý, sahne yükleniyor...");
        SceneManager.LoadScene("FinishScene");
    }
}