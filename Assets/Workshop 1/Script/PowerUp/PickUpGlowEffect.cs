using System.Collections;
using UnityEngine;

public class PickupGlowEffect : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.4f;

    [SerializeField]
    private Vector3 startScale =
        Vector3.one;

    [SerializeField]
    private Vector3 endScale =
        new Vector3(6f, 6f, 6f);

    private MeshRenderer meshRenderer;

    private Material materialInstance;

    private Color originalColor;

    private void Awake()
    {
        meshRenderer =
            GetComponent<MeshRenderer>();

        materialInstance =
            meshRenderer.material;

        originalColor =
            materialInstance.color;
    }

    public void PlayEffect()
    {
        StopAllCoroutines();

        StartCoroutine(AnimateEffect());
    }

    private IEnumerator AnimateEffect()
    {
        gameObject.SetActive(true);

        float time = 0;

        transform.localScale = startScale;

        Color color = originalColor;

        while (time < duration)
        {
            time += Time.deltaTime;

            float progress =
                time / duration;

            // Membesar
            transform.localScale =
                Vector3.Lerp(
                    startScale,
                    endScale,
                    progress
                );

            // Fade
            color.a =
                Mathf.Lerp(
                    originalColor.a,
                    0,
                    progress
                );

            materialInstance.color =
                color;

            yield return null;
        }

        // Reset alpha
        materialInstance.color =
            originalColor;

        gameObject.SetActive(false);
    }
}