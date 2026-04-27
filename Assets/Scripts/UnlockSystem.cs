using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AromaData
{
    public string name;
    public int unlockLevel;
}

public class UnlockSystem : MonoBehaviour
{
    public List<AromaData> allAromas;
    public List<string> unlockedAromas = new List<string>();

    public List<AromaButton> aromaButtons;

    public void ApplyUnlocks()
    {
        foreach (var btn in aromaButtons)
        {
            if (unlockedAromas.Contains(btn.aromaName))
                btn.Unlock();
            else
                btn.Lock();
        }
    }

    public void CheckUnlock(int level)
    {
        foreach (var aroma in allAromas)
        {
            if (level >= aroma.unlockLevel &&
                !unlockedAromas.Contains(aroma.name))
            {
                unlockedAromas.Add(aroma.name);
                Debug.Log("Açýldý: " + aroma.name);
            }
        }

        ApplyUnlocks();
    }
}