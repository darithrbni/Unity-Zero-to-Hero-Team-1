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
