using UnityEngine;
using UnityEngine.UI; // For UI slider

public class PlayerEnergy : MonoBehaviour
{
    [Header("Energy Settings")]
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDepletionRate = 10f; // Energy per second while moving

    [Header("References")]
    public PlayerMovement playerMovement; // Reference to your movement script
    public Slider energyBar; // Reference to UI slider

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnergy = maxEnergy;
        if (energyBar != null)
            energyBar.maxValue = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement != null && playerMovement.IsMoving())
        {
            currentEnergy -= energyDepletionRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

        if (energyBar != null)
            energyBar.value = currentEnergy;

        // Optional: Prevent movement if out of energy
        if (currentEnergy <= 0 && playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        else if (currentEnergy > 0 && playerMovement != null && !playerMovement.enabled)
        {
            playerMovement.enabled = true;
        }
    }

    // Optional: Call this to restore energy
    public void RestoreEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
    }

    // Returns true if the player has enough energy to interact
    public bool CanInteract()
    {
        return currentEnergy > 10f; // Set your threshold here
    }

    // Returns the current energy as a percentage (0 to 1)
    public float GetEnergyPercent()
    {
        return currentEnergy / maxEnergy;
    }
}
