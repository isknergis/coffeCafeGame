using System.Collections.Generic;
using UnityEngine;

public class AromaSelection : MonoBehaviour
{
    public List<string> selectedAromas = new List<string>();

    public void ToggleAroma(string aroma)
    {
        if (selectedAromas.Contains(aroma))
            selectedAromas.Remove(aroma);
        else
            selectedAromas.Add(aroma);
    }

    public void ResetAromas()
    {
        selectedAromas.Clear();
    }
}