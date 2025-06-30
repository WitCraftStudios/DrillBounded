using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class RocketLaunchPlace : MonoBehaviour
{
    [Header("Rocket Launch Settings")]
    public GameObject rocketPrefab;      // Assign your rocket prefab in the inspector
    public Transform launchPoint;        // Assign a child transform as the launch position

    [Header("Gameplay Settings")]
    public float cooldownTime = 10f;     // Cooldown in seconds
    public int currencyPerBattery = 10;  // Reward per battery delivered

    public bool isBroken = false;
    public float repairTime = 10f;
    public Image repairCircle; // Assign in Inspector
    private bool playerNearby = false;
    private bool isRepairing = false;
    private float repairTimer = 0f;

    private float cooldownTimer = 0f;
    private bool rocketInFlight = false;
    private int pendingBatteries = 0;
    private PlayerInventory playerInventory;
    private PlayerInventory storedPlayerInventory; // Store reference for rocket return
    public GameObject interactionPrompt;

    void Update()
    {
        // Handle cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0f)
                cooldownTimer = 0f;
        }

        if (isBroken && playerNearby && !isRepairing && UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            StartCoroutine(RepairRoutine());
        }
        if (isRepairing && repairCircle != null)
        {
            repairCircle.fillAmount = repairTimer / repairTime;
        }

        // Handle launch input
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryLaunchRocket();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            playerInventory = other.GetComponent<PlayerInventory>();
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerInventory = null;
            interactionPrompt.SetActive(false);
        }
    }

    private IEnumerator RepairRoutine()
    {
        isRepairing = true;
        repairTimer = 0f;
        if (repairCircle != null)
        {
            repairCircle.gameObject.SetActive(true);
            repairCircle.fillAmount = 0f;
        }
        while (repairTimer < repairTime)
        {
            repairTimer += Time.deltaTime;
            if (repairCircle != null)
                repairCircle.fillAmount = repairTimer / repairTime;
            yield return null;
        }
        isBroken = false;
        isRepairing = false;
        if (repairCircle != null)
            repairCircle.gameObject.SetActive(false);
        Debug.Log("RocketLaunchPlace repaired!");
    }

    void TryLaunchRocket()
    {
        if (isBroken) {
            Debug.Log("RocketLaunchPlace is broken! Repair it first.");
            return;
        }
        if (cooldownTimer > 0f)
        {
            Debug.Log("Rocket is on cooldown! Wait " + Mathf.CeilToInt(cooldownTimer) + " seconds.");
            return;
        }

        if (rocketInFlight)
        {
            Debug.Log("Rocket is already in flight!");
            return;
        }

        if (playerInventory == null)
        {
            Debug.Log("No player inventory found!");
            return;
        }

        if (playerInventory.batteryCount <= 0)
        {
            Debug.Log("No batteries to deliver!");
            return;
        }

        // Remove batteries, but don't give reward yet
        pendingBatteries = playerInventory.DeliverAllBatteries();

        // Spawn and launch the rocket
        GameObject rocketObj = Instantiate(rocketPrefab, launchPoint.position, launchPoint.rotation);
        RocketFlight flight = rocketObj.GetComponent<RocketFlight>();
        rocketInFlight = true;
        cooldownTimer = cooldownTime;

        // Launch upward (or use your desired direction)
        flight.Launch(launchPoint.up, OnRocketReturn);

        // Store the player inventory reference for rocket return
        storedPlayerInventory = playerInventory;

        Debug.Log("Rocket launched to HQ!");
    }

    void OnRocketReturn()
    {
        if (storedPlayerInventory != null)
        {
            storedPlayerInventory.AddOxygenCylinders(1);
            Debug.Log("Rocket returned! Delivered " + pendingBatteries + " batteries to HQ! Received 1 oxygen cylinder.");
        }
        else
        {
            Debug.Log("Rocket returned, but no stored player inventory found to grant reward.");
        }
        rocketInFlight = false;
        pendingBatteries = 0;
        storedPlayerInventory = null; // Clear stored reference
    }

    public void BreakDown()
    {
        isBroken = true;
        Debug.Log("RocketLaunchPlace damaged by asteroid!");
        // Add additional damage logic here
    }

    public void Repair()
    {
        if (!isBroken || isRepairing) return;
        StartCoroutine(RepairRoutine());
    }
}