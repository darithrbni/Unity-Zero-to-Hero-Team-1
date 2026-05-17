using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage()
    {
        health--;

        Debug.Log("Player HP: " + health);

        if (health <= 0)
        {
            Debug.Log("PLAYER MATI");
        }
    }
}