using UnityEngine;

public class SpinUI : MonoBehaviour
{
    public float spinSpeed = 180f; // Degrees per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
