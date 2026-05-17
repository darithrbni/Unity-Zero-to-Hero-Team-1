using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private GameObject hitEffectPrefab;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth player =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            Instantiate(
            hitEffectPrefab,
            transform.position,
            Quaternion.identity
        );

        player.TakeDamage();

        Destroy(gameObject);
        }
    }
}