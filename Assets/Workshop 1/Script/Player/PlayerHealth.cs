using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private HealthUI healthUI;

    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        healthUI.UpdateHealth(health);
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

            Destroy(gameObject);
        }
    }
}