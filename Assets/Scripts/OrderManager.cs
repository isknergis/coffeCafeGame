using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [Header("Order")]
    public List<string> coffeeTypes;
    public List<string> unlockedAromas;

    public int playerLevel = 1;
    public CoffeeOrder currentOrder;

    [Header("UI")]
    public Text orderText;
    public Image patienceBar;

    [Header("Patience")]
    public float patience = 10f;
    float currentTime;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>(); // yeni yöntem
    }

    void Update()
    {
        if (currentOrder == null) return;

        currentTime -= Time.deltaTime;

        // bar güncelle
        if (patienceBar != null)
            patienceBar.fillAmount = currentTime / patience;

        // süre bitince
        if (currentTime <= 0)
        {
            Debug.Log("Süre doldu!");

            if (gameManager != null)
                gameManager.OnOrderFailed();

            GenerateOrder();
        }
    }

    public void GenerateOrder()
    {
        currentOrder = new CoffeeOrder();

        // kahve + þeker
        currentOrder.coffeeType = coffeeTypes[Random.Range(0, coffeeTypes.Count)];
        currentOrder.sugarLevel = Random.Range(0, 3);

        // ?? MULTI AROMA (LEVEL YOK)
        currentOrder.aromas = new List<string>();

        if (unlockedAromas != null && unlockedAromas.Count > 0)
        {
            // maksimum kaç aroma gelebilir (sabit)
            int maxAroma = Mathf.Min(2, unlockedAromas.Count);

            int aromaCount = Random.Range(0, maxAroma + 1);

            for (int i = 0; i < aromaCount; i++)
            {
                string a = unlockedAromas[Random.Range(0, unlockedAromas.Count)];

                if (!currentOrder.aromas.Contains(a))
                    currentOrder.aromas.Add(a);
            }
        }

        currentTime = patience;

        UpdateUI();

        Debug.Log("Sipariþ: " + currentOrder.coffeeType +
                  " | Þeker: " + currentOrder.sugarLevel +
                  " | Aromalar: " + string.Join(",", currentOrder.aromas));
    }
    void UpdateUI()
    {
        if (orderText == null) return;

        string sugarText = currentOrder.sugarLevel == 0 ? "Sade" :
                           currentOrder.sugarLevel == 1 ? "Orta" : "Çok";

        string aromaText = "Yok";

        if (currentOrder.aromas != null && currentOrder.aromas.Count > 0)
        {
            aromaText = string.Join(", ", currentOrder.aromas);
        }

        orderText.text =
            currentOrder.coffeeType + "\n" +
            "Þeker: " + sugarText + "\n" +
            "Aroma: " + aromaText;
    }
}