using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private HealthUI healthUI;

    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        healthUI.UpdateHealth(
    health,
    false
);
    }

    public void TakeDamage()
    {
        health--;
        healthUI.UpdateHealth(health);

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
        if (health >= 3)
        {
            return;
        }

        health += amount;

        health = Mathf.Clamp(health, 0, 3);

        healthUI.UpdateHealth(health);

        Debug.Log("Player Heal: " + health);
    }
}