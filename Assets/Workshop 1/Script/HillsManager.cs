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
