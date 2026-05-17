using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHeart;

    [SerializeField] private Sprite emptyHeart;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition =
            transform.localPosition;
    }

    public void UpdateHealth(
    int currentHealth,
    bool playShake = true
)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
        if (playShake)
        {
            StartCoroutine(ShakeHearts());
        }
    }

    private IEnumerator ShakeHearts()
    {
        float duration = 0.3f;

        float strength = 4f;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            Vector3 randomOffset =
                new Vector3(
                    Random.Range(-strength, strength),
                    Random.Range(-strength, strength),
                    0
                );

            transform.localPosition =
                originalPosition + randomOffset;

            yield return null;
        }

        transform.localPosition =
            originalPosition;
    }
}