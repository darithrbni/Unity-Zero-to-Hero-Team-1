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