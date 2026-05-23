using System.Collections;
using UnityEngine;

public class PickupFlash : MonoBehaviour
{
    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private float flashIntensity = 100f;

    [SerializeField]
    private float flashDuration = 0.5f;

    public void Flash()
    {
        StopAllCoroutines();

        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        pointLight.intensity =
            flashIntensity;

        float time = 0;

        while (time < flashDuration)
        {
            time += Time.deltaTime;

            pointLight.intensity =
                Mathf.Lerp(
                    flashIntensity,
                    0,
                    time / flashDuration
                );

            yield return null;
        }

        pointLight.intensity = 0;
    }
}