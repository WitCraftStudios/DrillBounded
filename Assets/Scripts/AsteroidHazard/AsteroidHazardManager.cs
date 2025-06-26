using UnityEngine;

public class AsteroidHazardManager : MonoBehaviour
{
    public GameObject asteroidPrefab; // Assign your big asteroid prefab
    public Transform groundTarget;    // Assign your ground object in Inspector
    public float spawnHeight = 30f;   // How high above the ground to spawn
    public float asteroidSpeed = 5f;  // How fast the asteroid moves
    public float attackInterval = 30f; // Time between attacks

    [Header("UI")]
    public GameObject warningSignUI; // The icon
    public GameObject warningTextUI; // The text

    private GameObject activeAsteroid = null;
    private float timer = 0f;
    private bool isPaused = false;

    void Update()
    {
        if (isPaused) return;
        timer += Time.deltaTime;
        if (timer >= attackInterval && activeAsteroid == null)
        {
            timer = 0f;
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        if (groundTarget == null) return;
        if (warningSignUI != null) warningSignUI.SetActive(true);
        if (warningTextUI != null) warningTextUI.SetActive(true);
        Vector3 spawnPos = groundTarget.position + Vector3.up * spawnHeight;
        activeAsteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        AsteroidHazard hazard = activeAsteroid.GetComponent<AsteroidHazard>();
        if (hazard != null)
        {
            Vector3 moveDir = Vector3.down;
            hazard.Initialize(moveDir, asteroidSpeed);
            Debug.Log($"Asteroid initialized with direction {moveDir} and speed {asteroidSpeed}");
        }
    }

    public void ClearAsteroid()
    {
        if (activeAsteroid != null)
        {
            Destroy(activeAsteroid);
            activeAsteroid = null;
        }
        if (warningSignUI != null) warningSignUI.SetActive(false);
        if (warningTextUI != null) warningTextUI.SetActive(false);
    }

    public bool IsHazardActive()
    {
        return activeAsteroid != null;
    }

    public void PauseHazard() { isPaused = true; }
    public void ResumeHazard() { isPaused = false; }
}
