using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int batteryCount = 0;
    public int currency = 0; // Or use upgradePoints if you prefer
    public int oxygenCylinderCount = 0;

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

    public void AddOxygenCylinders(int amount)
    {
        oxygenCylinderCount += amount;
        Debug.Log("Received " + amount + " oxygen cylinders! Total: " + oxygenCylinderCount);
        // Optionally, update UI here
    }

    public bool SpendOxygenCylinder()
    {
        if (oxygenCylinderCount > 0)
        {
            oxygenCylinderCount--;
            Debug.Log("Spent 1 oxygen cylinder. Remaining: " + oxygenCylinderCount);
            // Optionally, update UI here
            return true;
        }
        Debug.Log("No oxygen cylinders to spend!");
        return false;
    }
}