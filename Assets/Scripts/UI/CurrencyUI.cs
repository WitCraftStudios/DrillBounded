using UnityEngine;
using TMPro; // Use UnityEngine.UI if using legacy Text

public class CurrencyUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text currencyText; // Use Text if using legacy UI

    // Update is called once per frame
    void Update()
    {
        if (playerInventory != null && currencyText != null)
        {
            currencyText.text = playerInventory.currency.ToString();
        }
    }
}
