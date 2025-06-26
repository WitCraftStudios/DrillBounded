using UnityEngine;

public class MinigameAsteroidMover : MonoBehaviour
{
    public float speed = 200f; // Units per second
    private RectTransform rectTransform;
    private RectTransform playArea;
    private bool initialized = false;
    private AsteroidMinigameManager manager;

    public void Initialize(RectTransform playAreaRect, AsteroidMinigameManager minigameManager)
    {
        playArea = playAreaRect;
        rectTransform = GetComponent<RectTransform>();
        manager = minigameManager;
        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;
        rectTransform.anchoredPosition += Vector2.right * speed * Time.deltaTime;
        // Destroy if out of play area (right side)
        if (rectTransform.anchoredPosition.x > playArea.rect.width / 2 + 50)
        {
            if (manager != null) manager.OnAsteroidDestroyed(gameObject, true);
            Destroy(gameObject);
        }
    }
} 