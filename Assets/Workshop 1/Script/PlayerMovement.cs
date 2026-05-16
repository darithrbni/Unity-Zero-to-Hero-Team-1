using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;

    private Quaternion angle;
    [SerializeField] private float smoothTurn;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Tugas, supaya pesawat tidak keluar dari border kamera

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        angle.x = Mathf.Lerp(angle.x, moveX * moveSpeed, smoothTurn * Time.deltaTime);
        angle.y = Mathf.Lerp(angle.y, moveY * moveSpeed, smoothTurn * Time.deltaTime);

        angle.x = Mathf.Clamp(angle.x, -55, 55);
        angle.y = Mathf.Clamp(angle.y, -25, 25);

        transform.rotation = Quaternion.Euler(-angle.y, 0, -angle.x);

        Debug.Log(movement);
    }
}
