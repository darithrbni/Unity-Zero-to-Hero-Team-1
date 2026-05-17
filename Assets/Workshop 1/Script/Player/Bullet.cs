using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
enemy.DestroyEnemy();

Destroy(gameObject);
        }
    }
}