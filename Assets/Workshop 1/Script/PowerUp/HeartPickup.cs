using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    [SerializeField] private float rotateSpeed = 100f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Bergerak ke belakang
        transform.position +=
            Vector3.back *
            moveSpeed *
            Time.deltaTime;

        // Muter
        transform.Rotate(
            0,
            rotateSpeed * Time.deltaTime,
            0
        );

        // Hapus kalau sudah lewat kamera
        if (
            transform.position.z <
            mainCamera.transform.position.z - 10f
        )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player =
            other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.Heal(1);

            Destroy(gameObject);
        }
    }
}