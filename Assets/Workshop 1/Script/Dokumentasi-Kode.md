PlayerMovement.cs
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;

    private Quaternion angle;
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

        angle.x = Mathf.Lerp(angle.x, moveX * moveSpeed, smoothTurn * Time.deltaTime);
        angle.y = Mathf.Lerp(angle.y, moveY * moveSpeed, smoothTurn * Time.deltaTime);

        angle.x = Mathf.Clamp(angle.x, -55, 55);
        angle.y = Mathf.Clamp(angle.y, -25, 25);

        transform.rotation = Quaternion.Euler(-angle.y, 0, -angle.x);

        Debug.Log(movement);
    }
}



HillsMovement.cs
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

        if(transform.position.z < camera.transform.position.z)
        {
            // Tugas, ubah destroy spawn jadi disable enable
            
            // Setactive(false)
            Destroy(gameObject);

            // Setactive(true)
            HillsManager.Instance.SpawnHill();
        }
    }
}




HillsManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class HillsManager : MonoBehaviour
{
    public GameObject[] Hills;
    public float speed = 20;
    public Transform spawnPoint;

    public static HillsManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHill()
    {
        int index = Random.Range(0, Hills.Length);
        Instantiate(Hills[index], spawnPoint);
    }
}
