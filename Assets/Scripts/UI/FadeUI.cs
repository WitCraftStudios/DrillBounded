using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUI : MonoBehaviour
{
    public Image fadeImage; // Assign a full-screen UI Image (black, alpha 0)
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (fadeImage != null)
            fadeImage.gameObject.SetActive(true);
        SetAlpha(0f);
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0f, 1f, onComplete));
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1f, 0f, onComplete));
    }

    private IEnumerator Fade(float from, float to, System.Action onComplete)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(to);
        onComplete?.Invoke();
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
} 