using System.Collections;

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    private Vector3 targetPosition;

    private Vector3 previousPosition;

    private float angleX;
    private float angleY;

    private bool reachedTarget = false;

    private Vector3 patrolCenter;

    private Vector3 patrolDirection;

    [SerializeField]
    private float patrolRadius = 6f;

    [SerializeField]
    private float patrolSpeed = 2f;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;

        patrolCenter = target;

        previousPosition = transform.position;
    }

    private float currentTiltX;

    private float currentTiltY;

    void Update()
    {
        Vector3 oldPosition = transform.position;

        // Masuk arena
        if (!reachedTarget)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                2f * Time.deltaTime
            );

            if (
                Vector3.Distance(
                    transform.position,
                    targetPosition
                ) < 1f
            )
            {
                reachedTarget = true;

                ChooseNewDirection();

                StartCoroutine(RandomMovement());
            }
        }

        // Patrol
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                2f * Time.deltaTime
            );
        }

        // HITUNG ARAH GERAK
        Vector3 moveDirection =
            transform.position - oldPosition;

        float moveX = moveDirection.x;

        float moveY = moveDirection.y;

        // ROTASI PESAWAT
        float targetTiltX = Mathf.Clamp(
            moveX * 300f,
            -30f,
            30f
        );

        float targetTiltY = Mathf.Clamp(
            moveY * 300f,
            -45f,
            45f
        );

        currentTiltX = Mathf.Lerp(
            currentTiltX,
            targetTiltX,
            5f * Time.deltaTime
        );

        currentTiltY = Mathf.Lerp(
            currentTiltY,
            targetTiltY,
            5f * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(
            -currentTiltY,
            180,
            currentTiltX
        );
    }

    private void ChooseNewDirection()
    {
        patrolDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            0
        ).normalized;
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            Vector3 nextPosition =
                transform.position +
                patrolDirection * patrolSpeed;

            float distanceFromCenter =
                Vector3.Distance(
                    nextPosition,
                    patrolCenter
                );

            // Kalau hampir keluar radius
            if (distanceFromCenter > patrolRadius)
            {
                ChooseNewDirection();
            }

            targetPosition =
            transform.position +
            patrolDirection * 3f;

            float clampX = Mathf.Clamp(
                targetPosition.x,
                -15f,
                15f
            );

            float clampY = Mathf.Clamp(
                targetPosition.y,
                25f,
                45f
            );

            targetPosition = new Vector3(
                clampX,
                clampY,
                targetPosition.z
            );

            // Kadang random ganti arah
            if (Random.value > 0.7f)
            {
                ChooseNewDirection();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}