using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float moveY = 2f;
    public float rotateDuration = 0.3f; // ��ת����ʱ�䣨�룩

    private static MouseMove currentSelected = null;

    private bool isSelected = false;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private Vector3 mouseOffset;

    private int rotateStep = 0;
    private const int totalSteps = 8;

    // ��¼��ǰ�����棨trueΪ���棬falseΪ���棩
    private bool isFront = true;

    // ƽ����ת���
    private Quaternion targetRotation;
    private bool isRotating = false;
    private float rotateTimer = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        targetRotation = transform.rotation;

        // �жϳ�ʼ������
        float x = Mathf.Round(transform.rotation.eulerAngles.x);
        isFront = Mathf.Approximately(x, 270f) || Mathf.Approximately(x, -90f);
    }

    void Update()
    {
        if (!isSelected)
        {
            // ֻ��δѡ��ʱ���ż����ѡ��
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform == transform)
                    {
                        if (currentSelected != null && currentSelected != this)
                        {
                            currentSelected.Deselect();
                        }
                        isSelected = true;
                        currentSelected = this;
                        originalPosition = transform.position;
                        Plane plane = new Plane(Vector3.up, new Vector3(0, originalPosition.y + moveY, 0));
                        if (plane.Raycast(ray, out float distance))
                        {
                            Vector3 hitPoint = ray.GetPoint(distance);
                            mouseOffset = hitPoint - transform.position;
                        }
                        transform.position = new Vector3(transform.position.x, transform.position.y + moveY, transform.position.z);
                    }
                }
            }
        }
        else
        {
            // ��ק�У��������ٴΰ��������
            if (Input.GetMouseButtonDown(0))
            {
                isSelected = false;
                currentSelected = null;
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
            }
            else
            {
                // ��ק�������
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, new Vector3(0, originalPosition.y + moveY, 0));
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Vector3 targetPos = hitPoint - mouseOffset;
                    transform.position = new Vector3(targetPos.x, originalPosition.y + moveY, targetPos.z);
                }

                // ��T����y��ְ˷���ת
                if (Input.GetKeyDown(KeyCode.R) && !isRotating)
                {
                    rotateStep = (rotateStep + 1) % totalSteps;
                    float yAngle = 45f * rotateStep;
                    float xAngle = isFront ? -90f : 90f;
                    targetRotation = Quaternion.Euler(xAngle, yAngle, 0);
                    isRotating = true;
                    rotateTimer = 0f;
                }

                // ��Y�����������л���x��-90<->90��
                if (Input.GetKeyDown(KeyCode.Y) && !isRotating)
                {
                    isFront = !isFront;
                    float yAngle = 45f * rotateStep;
                    float xAngle = isFront ? -90f : 90f;
                    targetRotation = Quaternion.Euler(xAngle, yAngle, 0);
                    isRotating = true;
                    rotateTimer = 0f;
                }
            }
        }

        // ƽ����ת
        if (isRotating)
        {
            rotateTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotateTimer / rotateDuration);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            if (t >= 1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }

    public void Deselect()
    {
        isSelected = false;
        // ���޸�transform.position
    }
}
