using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float shootDelay = 2f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootDelay);

            Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.Euler(90, 180, 0)
            );
        }
    }
}