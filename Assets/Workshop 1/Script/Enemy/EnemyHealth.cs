using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    public void DestroyEnemy()
    {
        Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        Destroy(gameObject);
    }
}