using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    private Vector3 targetPosition;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
{
    transform.position = Vector3.Lerp(
        transform.position,
        targetPosition,
        2f * Time.deltaTime
    );
}
}