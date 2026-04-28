using UnityEngine;
using System.Collections;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject cupPrefab;
    public Transform spawnPoint;

    public Renderer machineRenderer;

    public float brewTime = 3f;

    bool isBrewing = false;
    bool coffeeReady = false;

    GameObject currentCup;
    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
        SetColor(Color.red);
    }

    public void StartBrewing()
    {
        if (isBrewing || coffeeReady)
            return;

        StartCoroutine(BrewCoffee());
    }

    IEnumerator BrewCoffee()
    {
        isBrewing = true;
        coffeeReady = false;

        float t = 0;

        while (t < brewTime)
        {
            t += Time.deltaTime;

            float lerp = t / brewTime;
            Color currentColor = Color.Lerp(Color.red, Color.yellow, lerp);
            SetColor(currentColor);

            float shake = 0.02f;
            transform.localPosition = originalPos + Random.insideUnitSphere * shake;

            yield return null;
        }

        transform.localPosition = originalPos;

        SetColor(Color.green);

        currentCup = Instantiate(cupPrefab, spawnPoint.position, Quaternion.identity);

        isBrewing = false;
        coffeeReady = true;

        Debug.Log("Kahve hazýr!");
    }

    public bool IsCoffeeReady()
    {
        return coffeeReady;
    }

    public bool IsBrewing()
    {
        return isBrewing;
    }

    public void ResetMachine()
    {
        coffeeReady = false;
        isBrewing = false;

        SetColor(Color.red);

        if (currentCup != null)
            Destroy(currentCup);
    }

    void SetColor(Color color)
    {
        if (machineRenderer != null)
            machineRenderer.material.color = color;
    }
}