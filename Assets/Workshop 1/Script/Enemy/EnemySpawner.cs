using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Combat Area")]
    [SerializeField] private Transform combatAreaCenter;

    [SerializeField] private Vector2 combatAreaSize = new Vector2(30, 20);

    [Header("Spawn Settings")]

    [SerializeField] private int maxEnemies = 5;

    [SerializeField] private float minimumDistance = 10f;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private List<Vector3> reservedTargets = new List<Vector3>();

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(1f, 5f)
            );

            // Hapus enemy null
            activeEnemies.RemoveAll(enemy => enemy == null);

            // Batasi jumlah enemy
            if (activeEnemies.Count >= maxEnemies)
            {
                continue;
            }

            // Refresh target reservation
            reservedTargets.Clear();

            foreach (GameObject activeEnemy in activeEnemies)
            {
                reservedTargets.Add(
                    activeEnemy.transform.position
                );
            }

            // Pilih spawn point random
            int enemyCount = Random.Range(1, 4);

            enemyCount = Mathf.Min(
                enemyCount,
                maxEnemies - activeEnemies.Count
            );

            List<int> usedSpawnIndexes =
            new List<int>();

            for (int i = 0; i < enemyCount; i++)
            {
                int randomIndex;

                do
                {
                    randomIndex =
                        Random.Range(0, spawnPoints.Length);
                }
                while (usedSpawnIndexes.Contains(randomIndex));

                usedSpawnIndexes.Add(randomIndex);

                Transform randomSpawn =
                    spawnPoints[randomIndex];

                Vector3 targetPos = Vector3.zero;

                bool validTarget = false;

                int attempts = 0;

                // Cari target yang valid
                while (!validTarget && attempts < 50)
                {
                    attempts++;

                    targetPos =
                        combatAreaCenter.position +
                        new Vector3(
                            Random.Range(
                                -combatAreaSize.x / 2,
                                combatAreaSize.x / 2
                            ),

                            Random.Range(
                                -combatAreaSize.y / 2,
                                combatAreaSize.y / 2
                            ),

                            0
                        );

                    validTarget = true;

                    foreach (Vector3 reserved in reservedTargets)
                    {
                        float distance =
                            Vector3.Distance(
                                targetPos,
                                reserved
                            );

                        if (distance < minimumDistance)
                        {
                            validTarget = false;
                            break;
                        }
                    }
                }

                // Kalau gagal cari posisi
                if (!validTarget)
                {
                    Debug.LogWarning(
                        "Gagal menemukan target enemy."
                    );

                    continue;
                }

                // Simpan target
                reservedTargets.Add(targetPos);

                // Spawn enemy
                GameObject newEnemy = Instantiate(
                    enemyPrefab,
                    randomSpawn.position,
                    Quaternion.Euler(0, 180, 0)
                );

                // Kasih target ke enemy
                EnemyMovement movement =
                    newEnemy.GetComponent<EnemyMovement>();

                movement.SetTarget(targetPos);

                // Simpan enemy aktif
                activeEnemies.Add(newEnemy);
            }
        }
    }
}