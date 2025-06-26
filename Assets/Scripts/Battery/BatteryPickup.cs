using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public int batteryValue = 1; // How many batteries this pickup is worth
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has a PlayerInventory component
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddBattery(batteryValue);
            Destroy(gameObject); // Remove the battery from the world
        }
    }
}
