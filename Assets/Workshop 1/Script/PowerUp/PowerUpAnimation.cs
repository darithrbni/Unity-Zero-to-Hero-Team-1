using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{
    public enum JenisPowerUP
    {
        SpreadShot,
        RapidFire,
        GiantBullet
    }

    public JenisPowerUP typeItem;
    public float duration = 10f;

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
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.GetComponentInChildren<PlayerShooting>();

            if (playerShooting != null)
            {
                switch (typeItem)
                {
                    case JenisPowerUP.SpreadShot:
                        playerShooting.ActivateSpreadShoot(duration);
                        break;
                    
                    case JenisPowerUP.RapidFire:
                        playerShooting.ActivateRapidFire(duration);
                        break;

                    case JenisPowerUP.GiantBullet:
                        playerShooting.ActivateGiantBullet(duration);
                        break;
                }

                Destroy(gameObject);   
            }
        }
    }
}
