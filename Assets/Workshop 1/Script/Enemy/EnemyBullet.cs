using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

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
            player.TakeDamage();

            Destroy(gameObject);
        }
    }
}