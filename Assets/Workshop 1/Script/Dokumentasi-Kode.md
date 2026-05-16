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
using UnityEngine;

public class HillsManager : MonoBehaviour
{
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
}


