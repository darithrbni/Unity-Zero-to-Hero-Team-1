using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private HealthUI healthUI;

    [SerializeField] private GameObject explosionPrefab;

    [SerializeField]
    private Renderer[] playerRenderers;

    [SerializeField]
    private PickupFlash pickupFlash;

    [SerializeField]
    private float invincibleDuration = 3f;

    private bool isInvincible = false;

    private void Start()
    {
        healthUI.UpdateHealth(
            health,
            false
        );
    }

    public void TakeDamage()
    {
        if (isInvincible)
        {
            return;
        }
        health--;
        healthUI.UpdateHealth(health);

        StartCoroutine(InvincibleBlink());

        Debug.Log("Player HP: " + health);

        if (health <= 0)
        {
            Debug.Log("PLAYER MATI");

            Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

            GameOverManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        pickupFlash.Flash();

        if (health >= 3)
        {
            return;
        }

        health += amount;

        health = Mathf.Clamp(health, 0, 3);

        healthUI.UpdateHealth(health);

        Debug.Log("Player Heal: " + health);
    }


    private IEnumerator InvincibleBlink()
    {
        isInvincible = true;

        float blinkInterval = 0.08f;

        float timer = 0;

        while (timer < invincibleDuration)
        {
            // Hilang
            foreach (Renderer renderer in playerRenderers)
            {
                renderer.enabled = false;
            }

            yield return new WaitForSeconds(
                blinkInterval
            );

            // Muncul
            foreach (Renderer renderer in playerRenderers)
            {
                renderer.enabled = true;
            }

            yield return new WaitForSeconds(
                blinkInterval
            );

            timer += blinkInterval * 2;
        }

        // Pastikan renderer nyala
        foreach (Renderer renderer in playerRenderers)
        {
            renderer.enabled = true;
        }

        isInvincible = false;
    }
}