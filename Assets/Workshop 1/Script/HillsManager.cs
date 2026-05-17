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
