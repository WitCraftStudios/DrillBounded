using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScopeCrosshairController : MonoBehaviour
{
    public RectTransform scopeRectTransform; // Assign your scope image (parent)
    public RectTransform crosshairRectTransform; // Assign your crosshair image (child)
    public Camera uiCamera; // Assign if using Screen Space - Camera, else leave null

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 localPoint;
        // Convert mouse position to local point in the scope's parent space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            scopeRectTransform, // Mouse position relative to scope
            mousePos,
            uiCamera,
            out localPoint
        );

        // Clamp localPoint to scope rect
        Rect scopeRect = scopeRectTransform.rect;
        localPoint.x = Mathf.Clamp(localPoint.x, scopeRect.xMin, scopeRect.xMax);
        localPoint.y = Mathf.Clamp(localPoint.y, scopeRect.yMin, scopeRect.yMax);

        // Set crosshair's anchoredPosition (relative to scope)
        crosshairRectTransform.anchoredPosition = localPoint;
    }
}
