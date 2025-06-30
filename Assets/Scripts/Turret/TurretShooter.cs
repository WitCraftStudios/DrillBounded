using UnityEngine;
using System.Collections;

public class TurretShooter : MonoBehaviour
{
    public ParticleSystem bulletParticle; // Assign in Inspector
    public float burstInterval = 1.5f;    // Time between bursts
    public float shotDelay = 0.1f;        // Delay between each bullet in a burst
    public int bulletsPerBurst = 3;

    private float timer = 0f;
    private bool isBursting = false;
    public GameObject interactionPrompt;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= burstInterval && !isBursting)
        {
            StartCoroutine(BurstFire());
            timer = 0f;
        }
    }

    IEnumerator BurstFire()
    {
        isBursting = true;
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            if (bulletParticle != null)
                bulletParticle.Play();
            yield return new WaitForSeconds(shotDelay);
        }
        isBursting = false;
    }
}