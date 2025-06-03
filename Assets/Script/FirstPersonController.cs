using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float jumpForce = 5.0f;
    public float gravity = -9.81f;

    public float mouseSensitivity = 2.0f; // 鼠标灵敏度
    public Transform playerCamera; // 拖拽主摄像机到此

    private float xRotation = 0f;
    private float currentSpeed;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning = false;

    public Rigidbody rb;

    void Start()
    {
        currentSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
    }

    void Update()
    {
        // 鼠标转向
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 左右旋转角色
        transform.Rotate(Vector3.up * mouseX);

        // 上下旋转摄像机
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 移动
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = walkSpeed;
            isRunning = false;
        }

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        rb.MovePosition(rb.position + move * currentSpeed * Time.deltaTime);
        rb.velocity = new Vector3(rb.velocity.x, velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
