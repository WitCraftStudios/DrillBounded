using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractionPromptUI : MonoBehaviour
{
    public static InteractionPromptUI Instance;

    public GameObject panel;
    public Transform actionsContainer; // Parent for key/action UI elements
    public GameObject actionPrefab;    // Prefab with Image (key), TMP_Text (key label), TMP_Text (action)

    private readonly List<GameObject> activePrompts = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        HidePrompt();
    }

    public void ShowPrompt(List<(string key, string description, Sprite keySprite)> actions)
    {
        HidePrompt(); // Clear previous
        panel.SetActive(true);
        foreach (var (key, description, keySprite) in actions)
        {
            GameObject go = Instantiate(actionPrefab, actionsContainer);
            var image = go.GetComponentInChildren<Image>();
            var texts = go.GetComponentsInChildren<TMP_Text>(true);
            // Find key label TMP_Text (child of image)
            TMP_Text keyLabel = null;
            TMP_Text actionLabel = null;
            foreach (var t in texts)
            {
                if (t.transform.parent == image.transform)
                    keyLabel = t;
                else
                    actionLabel = t;
            }
            if (image != null && keySprite != null)
                image.sprite = keySprite;
            if (keyLabel != null)
                keyLabel.text = key;
            if (actionLabel != null)
                actionLabel.text = description;
            activePrompts.Add(go);
        }
    }

    public void HidePrompt()
    {
        panel.SetActive(false);
        foreach (var go in activePrompts)
        {
            Destroy(go);
        }
        activePrompts.Clear();
    }
} 