using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameObject heartPickupPrefab;

    [SerializeField]
    [Range(0f, 1f)]
    private float heartDropChance = 0.1f;

    public void DestroyEnemy()
    {
        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        // Chance drop heart
        if (Random.value <= heartDropChance)
        {
            Instantiate(
                heartPickupPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        ScoreManager.Instance.AddScore(100);
        Destroy(gameObject);
    }
}