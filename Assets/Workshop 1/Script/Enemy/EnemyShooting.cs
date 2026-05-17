using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(1f, 3f)
            );

            Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.Euler(90, 180, 0)
            );
        }
    }
}