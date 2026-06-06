using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameObject heartPickupPrefab;
    [SerializeField] private GameObject spreadShootPrefab;
    [SerializeField] private GameObject rapidFirePrefab;
    [SerializeField] private GameObject giantBulletPrefab;

    [SerializeField] [Range(0f, 1f)] private float heartDropChance = 0.1f;
    [SerializeField] [Range(0f, 1f)] private float spreadDropChance = 0.1f;
    [SerializeField] [Range(0f, 1f)] private float rapidDropChance = 0.1f;
    [SerializeField] [Range(0f, 1f)] private float giantBulletChance = 0.1f;

    public void DestroyEnemy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        float randomRoll = Random.value;

        // Chance drop heart
        if (randomRoll <= heartDropChance)
        {
            Instantiate(heartPickupPrefab, transform.position, Quaternion.identity);
        }
        else if (randomRoll <= (heartDropChance + spreadDropChance))
        {
            Instantiate(spreadShootPrefab, transform.position, Quaternion.identity);
        }
        else if (randomRoll <= (heartDropChance + spreadDropChance + rapidDropChance))
        {
            Instantiate(rapidFirePrefab, transform.position, Quaternion.identity);
        }
        else if (randomRoll <= (heartDropChance + spreadDropChance + rapidDropChance + giantBulletChance))
        {
            Instantiate(giantBulletPrefab, transform.position, Quaternion.identity);
        }

        ScoreManager.Instance.AddScore(100);
        Destroy(gameObject);
    }
}