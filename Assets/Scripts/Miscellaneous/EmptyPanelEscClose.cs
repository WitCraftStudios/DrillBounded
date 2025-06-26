using UnityEngine;
using UnityEngine.InputSystem;

public class EmptyPanelEscClose : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            gameObject.SetActive(false);
        }
    }
}
