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






## HillsManager.cs
using System.Collections;
using UnityEngine;

public class HillsManager : MonoBehaviour
{
    public GameObject[] Enemies;

    public Transform spawnPoint;
    public static HillsManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3f);
        foreach(GameObject Enemy in Enemies)
        {
            Enemy.SetActive(true);
            // Tugas, kalau enemy sudah habis di array, panggil lagi SpawnEnemies()
        }
    }
}



## EnemyMovement.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    private Vector3 resultPos;
    
    private void OnEnable()
    {
        Vector3 offset = new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 15f), 0);
        resultPos = transform.localPosition + offset;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, resultPos, 3f * Time.deltaTime);
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




## Bullet.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * 10f * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
        if(collision != null)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
