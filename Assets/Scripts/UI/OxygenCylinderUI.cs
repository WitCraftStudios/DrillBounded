using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenCylinderUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text oxygenCountText;
    public Image oxygenCylinderImage;
    public Image fillOverlayImage;
    public int maxCylinders = 10;

    // Update is called once per frame
    void Update()
    {
        if (playerInventory != null && oxygenCountText != null && oxygenCylinderImage != null && fillOverlayImage != null)
        {
            int count = playerInventory.oxygenCylinderCount;
            oxygenCountText.text = count.ToString();
            // Always show the images
            oxygenCylinderImage.enabled = true;
            fillOverlayImage.enabled = true;
            fillOverlayImage.fillAmount = Mathf.Clamp01((float)count / maxCylinders);
        }
    }
}
