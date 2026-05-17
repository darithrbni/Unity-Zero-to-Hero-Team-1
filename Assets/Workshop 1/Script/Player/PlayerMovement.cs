using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveX;
    private float moveY;

    private float angleX;
    private float angleY;

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

        float clampX = Mathf.Clamp(transform.position.x, -15, 15);
    float clampY = Mathf.Clamp(transform.position.y, 20, 45);

        transform.position = new Vector3(clampX, clampY, transform.position.z);

       angleX = Mathf.Lerp(angleX, moveX * moveSpeed, smoothTurn * Time.deltaTime);
        angleY = Mathf.Lerp(angleY, moveY * moveSpeed, smoothTurn * Time.deltaTime);

        angleX = Mathf.Clamp(angleX, -55, 55);
        angleY = Mathf.Clamp(angleY, -25, 25);

        transform.rotation = Quaternion.Euler(-angleY, 0, -angleX);
    }
}
