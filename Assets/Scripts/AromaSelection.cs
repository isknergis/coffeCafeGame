using System.Collections.Generic;
using UnityEngine;

public class AromaSelection : MonoBehaviour
{
    public List<string> selectedAromas = new List<string>();

    public UnlockSystem unlockSystem; // ?? kilit kontrolü

    public void ToggleAroma(string aroma)
    {
        // ?? kilitli mi?
        if (!unlockSystem.unlockedAromas.Contains(aroma))
        {
            Debug.Log("Kilitli aroma: " + aroma);
            return;
        }

        // ?? toggle sistemi
        if (selectedAromas.Contains(aroma))
        {
            selectedAromas.Remove(aroma);
            Debug.Log("Aroma çýkarýldý: " + aroma);
        }
        else
        {
            selectedAromas.Add(aroma);
            Debug.Log("Aroma eklendi: " + aroma);
        }
    }

    public void ResetAromas()
    {
        selectedAromas.Clear();
    }
}