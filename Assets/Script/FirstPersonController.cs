using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �ƶ��ٶ�
    public float mouseSensitivity = 2.0f; // ���������
    public Transform playerCamera; // ��ק�����������

    public float minFov = 30f; // ��С��Ұ
    public float maxFov = 90f; // �����Ұ
    public float fovStep = 5f; // ÿ�ι��ֵ����Ĳ���

    [Header("�Ƿ������ƶ�����ͨ��UI��ť����")]
    public bool canMove = true; // �Ƿ������ƶ���Inspector��ѡ

    private float xRotation = 0f;
    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // ��ȡCamera���
        cam = playerCamera.GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogWarning("playerCameraδ����Camera������޷�����FOV");
        }
        //Cursor.lockState = CursorLockMode.Locked; // �������
    }

    void Update()
    {
        // ���ת��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ������ת��ɫ
        transform.Rotate(Vector3.up * mouseX);

        // ������ת�����
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �ƶ�����ѡ��
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }

        // ���ֿ���FOV
        if (cam != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - scroll * fovStep * 10f, minFov, maxFov);
            }
        }
    }

    // UI��ť���ã������ƶ�
    public void EnableMove()
    {
        canMove = true;
    }

    // UI��ť���ã���ֹ�ƶ�
    public void DisableMove()
    {
        canMove = false;
    }

    // UI��ť���ã��л��ƶ�״̬
    public void ToggleMove()
    {
        canMove = !canMove;
    }
}
