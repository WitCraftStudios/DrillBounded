using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerSleepInteraction : MonoBehaviour
{
    private ISleepPlace currentSleepPlace;
    public PlayerEnergy playerEnergy;
    public FadeUI fadeUI; // Assign in inspector
    public float sleepFadeDuration = 1f;
    public float sleepTime = 2f; // In-game hours to skip (can be used for time system)
    public PlayerInventory playerInventory; // Assign in inspector

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Sleep.performed += OnSleep;
        inputActions.Player.Eat.performed += OnEat;
    }

    private void OnDisable()
    {
        inputActions.Player.Sleep.performed -= OnSleep;
        inputActions.Player.Eat.performed -= OnEat;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (inputActions.Player.Interact.triggered)
        {
            Debug.Log("Interact action triggered in Update!");
            Debug.Log("currentSleepPlace: " + currentSleepPlace);
            if (currentSleepPlace != null)
            {
                Debug.Log("currentSleepPlace is not null in Update.");
            }
            else
            {
                Debug.Log("currentSleepPlace is null in Update.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called with: {other.name}, tag: {other.tag}");
        currentSleepPlace = other.GetComponent<ISleepPlace>();
        if (currentSleepPlace != null)
        {
            Debug.Log("You can sleep here! Press the assigned key.");
            // Optionally, show UI prompt
        }
        else
        {
            Debug.Log("ISleepPlace not found on collider.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit called with: {other.name}, tag: {other.tag}");
        if (other.GetComponent<ISleepPlace>() == currentSleepPlace)
        {
            currentSleepPlace = null;
            Debug.Log("Left sleep place trigger. currentSleepPlace set to null.");
            // Optionally, hide UI prompt
        }
    }

    private void OnSleep(InputAction.CallbackContext context)
    {
        if (currentSleepPlace != null)
        {
            StartCoroutine(SleepSequence());
        }
    }

    private IEnumerator SleepSequence()
    {
        if (playerInventory == null || !playerInventory.SpendOxygenCylinder())
        {
            Debug.Log("Cannot sleep: No oxygen cylinders available!");
            yield break;
        }
        if (currentSleepPlace is SleepPlace sleepPlace && sleepPlace.isBroken)
        {
            Debug.Log("Cannot sleep: SleepPlace is broken!");
            yield break;
        }
        if (fadeUI != null)
        {
            fadeUI.fadeDuration = sleepFadeDuration;
            fadeUI.FadeOut();
            yield return new WaitForSecondsRealtime(sleepFadeDuration);
        }
        currentSleepPlace.Sleep(playerEnergy);
        yield return new WaitForSecondsRealtime(sleepTime);
        if (fadeUI != null)
        {
            fadeUI.FadeIn();
            yield return new WaitForSecondsRealtime(sleepFadeDuration);
        }
    }

    private void OnEat(InputAction.CallbackContext context)
    {
        if (currentSleepPlace != null)
        {
            currentSleepPlace.Eat(playerEnergy);
        }
    }
}
