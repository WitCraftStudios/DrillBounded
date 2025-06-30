using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDrillerInteraction : MonoBehaviour
{
    private Driller currentDriller;
    public GameObject interactionPrompt;

    void OnTriggerEnter(Collider other)
    {
        currentDriller = other.GetComponent<Driller>();
        if (currentDriller != null)
        {
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Driller>() == currentDriller)
        {
            currentDriller = null;
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (currentDriller != null)
        {
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                if (currentDriller.isBroken)
                {
                    currentDriller.Repair();
                }
            }
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (!currentDriller.isBroken)
                {
                    if (currentDriller.isRunning)
                    {
                        currentDriller.StopDriller();
                    }
                    else
                    {
                        currentDriller.StartDriller();
                    }
                }
            }
        }
    }
}
