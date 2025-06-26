using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Driller : MonoBehaviour
{
    public bool isRunning = false;
    public bool isBroken = false;
    public float repairTime = 10f;
    public Image repairCircle; // Assign in Inspector

    [Header("Mining Settings")]
    public float miningInterval = 5f; // Time in seconds to mine one battery
    private float miningTimer = 0f;

    [Header("Battery Settings")]
    public GameObject batteryPrefab; // Assign your Battery prefab in the Inspector
    public Transform spawnPoint;     // Where the battery will appear (can be the driller's position)

    private bool playerNearby = false;
    private bool isRepairing = false;
    private float repairTimer = 0f;

    void Update()
    {
        if (isBroken && playerNearby && !isRepairing && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(RepairRoutine());
        }
        if (isRepairing && repairCircle != null)
        {
            repairCircle.fillAmount = repairTimer / repairTime;
        }
        if (isBroken) return;

        if (isRunning && batteryPrefab != null)
        {
            miningTimer += Time.deltaTime;

            if (miningTimer >= miningInterval)
            {
                miningTimer = 0f;
                MineBattery();
            }
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
        Debug.Log("Driller repaired!");
    }

    void MineBattery()
    {
        if (isBroken) {
            Debug.Log("Driller is broken! Repair it first.");
            return;
        }
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up;
        Instantiate(batteryPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Battery mined!");
    }

    public void StartDriller()
    {
        if (isBroken) {
            Debug.Log("Driller is broken! Repair it first.");
            return;
        }
        if (!isRunning && !isBroken)
        {
            isRunning = true;
            Debug.Log("Driller started!");
        }
    }

    public void StopDriller()
    {
        if (isBroken) {
            Debug.Log("Driller is broken! Repair it first.");
            return;
        }
        if (isRunning)
        {
            isRunning = false;
            Debug.Log("Driller stopped!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Press 'E' to repair the Driller!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    public void BreakDown()
    {
        isBroken = true;
        Debug.Log("Driller damaged by asteroid!");
        // Add additional damage logic here
    }

    public void Repair()
    {
        if (!isBroken || isRepairing) return;
        StartCoroutine(RepairRoutine());
    }
}
