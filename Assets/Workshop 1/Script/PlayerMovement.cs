using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
private float moveX;
private float moveY;

[SerializeField] private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        Debug.Log(movement);
    }
}
