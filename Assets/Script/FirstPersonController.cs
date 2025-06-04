using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移动速度
    public float mouseSensitivity = 2.0f; // 鼠标灵敏度
    public Transform playerCamera; // 拖拽主摄像机到此

    public float minFov = 30f; // 最小视野
    public float maxFov = 90f; // 最大视野
    public float fovStep = 5f; // 每次滚轮调整的步长

    [Header("是否允许移动，可通过UI按钮控制")]
    public bool canMove = true; // 是否允许移动，Inspector可选

    private float xRotation = 0f;
    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // 获取Camera组件
        cam = playerCamera.GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogWarning("playerCamera未挂载Camera组件，无法调整FOV");
        }
        //Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标
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

        // 移动（可选）
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }

        // 滚轮控制FOV
        if (cam != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scroll * fovStep * 10f, minFov, maxFov);
            }
        }
    }

    // UI按钮调用：允许移动
    public void EnableMove()
    {
        canMove = true;
    }

    // UI按钮调用：禁止移动
    public void DisableMove()
    {
        canMove = false;
    }

    // UI按钮调用：切换移动状态
    public void ToggleMove()
    {
        canMove = !canMove;
    }
}
