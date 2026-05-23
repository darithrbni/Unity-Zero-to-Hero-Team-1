# Enemy
## EnemyBullet.cs
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




## EnemyHealth.cs
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




## EnemyMovement.cs
using System.Collections;

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    private Vector3 targetPosition;

    private Vector3 previousPosition;

    private float angleX;
    private float angleY;

    private bool reachedTarget = false;

    private Vector3 patrolCenter;

    private Vector3 patrolDirection;

    [SerializeField]
    private float patrolRadius = 6f;

    [SerializeField]
    private float patrolSpeed = 2f;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;

        patrolCenter = target;

        previousPosition = transform.position;
    }

    private float currentTiltX;

    private float currentTiltY;

    void Update()
    {
        Vector3 oldPosition = transform.position;

        // Masuk arena
        if (!reachedTarget)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                2f * Time.deltaTime
            );

            if (
                Vector3.Distance(
                    transform.position,
                    targetPosition
                ) < 1f
            )
            {
                reachedTarget = true;

                ChooseNewDirection();

                StartCoroutine(RandomMovement());
            }
        }

        // Patrol
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                2f * Time.deltaTime
            );
        }

        // HITUNG ARAH GERAK
        Vector3 moveDirection =
            transform.position - oldPosition;

        float moveX = moveDirection.x;

        float moveY = moveDirection.y;

        // ROTASI PESAWAT
        float targetTiltX = Mathf.Clamp(
            moveX * 300f,
            -30f,
            30f
        );

        float targetTiltY = Mathf.Clamp(
            moveY * 300f,
            -45f,
            45f
        );

        currentTiltX = Mathf.Lerp(
            currentTiltX,
            targetTiltX,
            5f * Time.deltaTime
        );

        currentTiltY = Mathf.Lerp(
            currentTiltY,
            targetTiltY,
            5f * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(
            -currentTiltY,
            180,
            currentTiltX
        );
    }

    private void ChooseNewDirection()
    {
        patrolDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0
        ).normalized;
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            Vector3 nextPosition =
                transform.position +
                patrolDirection * patrolSpeed;

            float distanceFromCenter =
                Vector3.Distance(
                    nextPosition,
                    patrolCenter
                );

            // Kalau hampir keluar radius
            if (distanceFromCenter > patrolRadius)
            {
                ChooseNewDirection();
            }

            targetPosition =
            transform.position +
            patrolDirection * 3f;

            float clampX = Mathf.Clamp(
                targetPosition.x,
                -15f,
                15f
            );

            float clampY = Mathf.Clamp(
                targetPosition.y,
                25f,
                45f
            );

            targetPosition = new Vector3(
                clampX,
                clampY,
                targetPosition.z
            );

            // Kadang random ganti arah
            if (Random.value > 0.7f)
            {
                ChooseNewDirection();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}




## EnemyShooting.cs
using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(1f, 3f)
            );

            Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.Euler(90, 180, 0)
            );
        }
    }
}



## EnemySpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Combat Area")]
    [SerializeField] private Transform combatAreaCenter;

    [SerializeField] private Vector2 combatAreaSize = new Vector2(30, 20);

    [Header("Spawn Settings")]

    [SerializeField] private int maxEnemies = 5;

    [SerializeField] private float minimumDistance = 10f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private List<Vector3> reservedTargets = new List<Vector3>();

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(3f, 5f)
            );

            // Hapus enemy null
            activeEnemies.RemoveAll(enemy => enemy == null);

            // Batasi jumlah enemy
            if (activeEnemies.Count >= maxEnemies)
            {
                continue;
            }

            // Refresh target reservation
            reservedTargets.Clear();

            foreach (GameObject activeEnemy in activeEnemies)
            {
                reservedTargets.Add(
                    activeEnemy.transform.position
                );
            }

            // Pilih spawn point random
            int enemyCount = Random.Range(1, 4);

            enemyCount = Mathf.Min(
                enemyCount,
                maxEnemies - activeEnemies.Count
            );

            List<int> usedSpawnIndexes =
            new List<int>();

            for (int i = 0; i < enemyCount; i++)
            {
                int randomIndex;

                do
                {
                    randomIndex =
                        Random.Range(0, spawnPoints.Length);
                }
                while (usedSpawnIndexes.Contains(randomIndex));

                usedSpawnIndexes.Add(randomIndex);

                Transform randomSpawn =
                    spawnPoints[randomIndex];

                Vector3 targetPos = Vector3.zero;

                bool validTarget = false;

                int attempts = 0;

                // Cari target yang valid
                while (!validTarget && attempts < 50)
                {
                    attempts++;

                    targetPos =
                        combatAreaCenter.position +
                        new Vector3(
                            Random.Range(
                                -combatAreaSize.x / 2,
                                combatAreaSize.x / 2
                            ),

                            Random.Range(
                                -combatAreaSize.y / 2,
                                combatAreaSize.y / 2
                            ),

                            0
                        );

                    validTarget = true;

                    foreach (Vector3 reserved in reservedTargets)
                    {
                        float distance =
                            Vector3.Distance(
                                targetPos,
                                reserved
                            );

                        if (distance < minimumDistance)
                        {
                            validTarget = false;
                            break;
                        }
                    }
                }

                // Kalau gagal cari posisi
                if (!validTarget)
                {
                    Debug.LogWarning(
                        "Gagal menemukan target enemy."
                    );

                    continue;
                }

                // Simpan target
                reservedTargets.Add(targetPos);

                // Spawn enemy
                GameObject newEnemy = Instantiate(
                    enemyPrefab,
                    randomSpawn.position,
                    Quaternion.Euler(0, 180, 0)
                );

                // Kasih target ke enemy
                EnemyMovement movement =
                    newEnemy.GetComponent<EnemyMovement>();

                movement.SetTarget(targetPos);

                // Simpan enemy aktif
                activeEnemies.Add(newEnemy);
            }
        }
    }
}





# Hills
## HillsManager.cs
using UnityEngine;

public class HillsManager : MonoBehaviour
{
    public Transform spawnPoint;

    public static HillsManager Instance;

    void Awake()
    {
        Instance = this;
    }
}



## HillsMovement.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillsMovement : MonoBehaviour
{
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.back * 20 * Time.deltaTime;
        transform.position += movement;

        if(transform.position.z < camera.transform.position.z + 10)
        {
            // Tugas, ubah destroy spawn jadi disable enable
            
            // Setactive(false)
           transform.position = HillsManager.Instance.spawnPoint.position;
        }
    }
}





# Player
## PlayerBullet.cs
using UnityEngine;

public class PlayerBullet : MonoBehaviour
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



## PlayerHealth.cs
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




## PlayerMovement.cs
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;

    private float angleX;
    private float angleY;

    [SerializeField] private float smoothTurn;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Tugas, supaya pesawat tidak keluar dari border kamera

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        float clampX = Mathf.Clamp(transform.position.x, -15, 15);
        float clampY = Mathf.Clamp(transform.position.y, 20, 45);

        transform.position = new Vector3(clampX, clampY, transform.position.z);

        angleX = Mathf.Lerp(angleX, moveX * moveSpeed, smoothTurn * Time.deltaTime);
        angleY = Mathf.Lerp(angleY, moveY * moveSpeed, smoothTurn * Time.deltaTime);

        angleX = Mathf.Clamp(angleX, -55, 55);
        angleY = Mathf.Clamp(angleY, -25, 25);

        transform.rotation = Quaternion.Euler(-angleY, 0, -angleX);
    }
}




## PlayerShooting.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(90, 0, 0));
        }
    }
}




# Managers
## GameOverManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private CanvasGroup gameOverCanvas;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        StartCoroutine(FadeGameOver());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
    private IEnumerator FadeGameOver()
    {
        float duration = 1f;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            gameOverCanvas.alpha =
                Mathf.Lerp(
                    0,
                    1,
                    time / duration
                );

            yield return null;
        }

        gameOverCanvas.alpha = 1;
    }

}





## HealthUI.cs
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHeart;

    [SerializeField] private Sprite emptyHeart;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition =
            transform.localPosition;
    }

    public void UpdateHealth(
    int currentHealth,
    bool playShake = true
)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
        if (playShake)
        {
            StartCoroutine(ShakeHearts());
        }
    }

    private IEnumerator ShakeHearts()
    {
        float duration = 0.3f;

        float strength = 4f;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            Vector3 randomOffset =
                new Vector3(
                    Random.Range(-strength, strength),
                    Random.Range(-strength, strength),
                    0
                );

            transform.localPosition =
                originalPosition + randomOffset;

            yield return null;
        }

        transform.localPosition =
            originalPosition;
    }
}



## ScoreManager.cs
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;

        scoreText.text = "Score : " + score;
    }
}



# PowerUp
## HeartPickup.cs
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