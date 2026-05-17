# Enemy



## EnemyMovement.cs
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    private Vector3 targetPosition;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
{
    transform.position = Vector3.Lerp(
        transform.position,
        targetPosition,
        2f * Time.deltaTime
    );
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
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private int maxEnemies = 5;

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
            yield return new WaitForSeconds(spawnDelay);

            activeEnemies.RemoveAll(enemy => enemy == null);

            if (activeEnemies.Count >= maxEnemies)
                continue;

            Transform randomSpawn = spawnPoints[
                Random.Range(0, spawnPoints.Length)
            ];

            Vector3 targetPos = Vector3.zero;

            bool validTarget = false;

            while (!validTarget)
            {
                targetPos = combatAreaCenter.position + new Vector3(
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
                    if (Vector3.Distance(targetPos, reserved) < 20f)
                    {
                        validTarget = false;
                        break;
                    }
                }
            }

            reservedTargets.Add(targetPos);

            GameObject enemy = Instantiate(
                enemyPrefab,
                randomSpawn.position,
                Quaternion.Euler(0, 180, 0)
            );

            EnemyMovement movement =
                enemy.GetComponent<EnemyMovement>();

            movement.SetTarget(targetPos);

            activeEnemies.Add(enemy);
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
## Bullet.cs
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
        EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();

        if (enemy != null)
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
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
    float clampY = Mathf.Clamp(transform.position.y, 25, 40);

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
