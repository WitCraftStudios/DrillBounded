using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDrillerInteraction : MonoBehaviour
{
    private Driller currentDriller;

    void OnTriggerEnter(Collider other)
    {
        currentDriller = other.GetComponent<Driller>();
        if (currentDriller != null)
        {
            Debug.Log("Press 'E' to interact with the driller.");
            // Optionally, show UI prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Driller>() == currentDriller)
        {
            currentDriller = null;
            // Optionally, hide UI prompt
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
