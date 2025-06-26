using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int batteryCount = 0;
    public int currency = 0; // Or use upgradePoints if you prefer

    public void AddBattery(int amount)
    {
        batteryCount += amount;
        Debug.Log("Picked up a battery! Total: " + batteryCount);
        // Optionally, update UI here
    }

    // Call this when delivering batteries to HQ
    public int DeliverAllBatteries()
    {
        int delivered = batteryCount;
        batteryCount = 0;
        Debug.Log("Delivered " + delivered + " batteries to HQ!");
        // Optionally, update UI here
        return delivered;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        Debug.Log("Received " + amount + " currency! Total: " + currency);
    }
}