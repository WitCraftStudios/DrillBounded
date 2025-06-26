using UnityEngine;
using System;

public class RocketFlight : MonoBehaviour
{
    public float flightDuration = 3f; // Time to fly away and return
    public float flightDistance = 20f; // How far the rocket flies

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer = 0f;
    private bool returning = false;
    private Action onReturnCallback;

    public void Launch(Vector3 direction, Action onReturn)
    {
        startPosition = transform.position;
        targetPosition = startPosition + direction.normalized * flightDistance;
        timer = 0f;
        returning = false;
        onReturnCallback = onReturn;
    }

    void Update()
    {
        if (!returning)
        {
            timer += Time.deltaTime;
            float t = timer / (flightDuration / 2f);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            if (t >= 1f)
            {
                returning = true;
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;
            float t = timer / (flightDuration / 2f);
            transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            if (t >= 1f)
            {
                onReturnCallback?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}