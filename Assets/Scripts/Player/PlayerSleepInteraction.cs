using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerSleepInteraction : MonoBehaviour
{
    private ISleepPlace currentSleepPlace;
    public PlayerEnergy playerEnergy;
    private InputSystem_Actions inputActions;
    public FadeUI fadeUI; // Assign in inspector
    public float sleepFadeDuration = 1f;
    public float sleepTime = 2f; // In-game hours to skip (can be used for time system)

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Sleep.performed += OnSleep;
        inputActions.Player.Eat.performed += OnEat;
    }

    void OnDisable()
    {
        inputActions.Player.Sleep.performed -= OnSleep;
        inputActions.Player.Eat.performed -= OnEat;
        inputActions.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        currentSleepPlace = other.GetComponent<ISleepPlace>();
        if (currentSleepPlace != null)
        {
            Debug.Log("You can sleep or eat here! Press the assigned keys.");
            // Optionally, show UI prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ISleepPlace>() == currentSleepPlace)
        {
            currentSleepPlace = null;
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

    private System.Collections.IEnumerator SleepSequence()
    {
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
