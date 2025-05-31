using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // ��ת���ĵ�
    public Vector3 rotateCenter = Vector3.zero;
    // ��ת��
    public Vector3 rotateAxis = Vector3.up;
    // ��ת�Ƕ�
    public float rotateAngle = 90f;
    // ��ת����ʱ�䣨�룩
    public float duration = 0.5f;

    // ��������
    public float minDistance = 2f;   // ��С����
    public float maxDistance = 20f;  // ������
    public float zoomSpeed = 5f;     // �����ٶ�

    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(RotateAroundPoint());
        }

        // �����ֵ���
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Zoom(scroll);
        }
    }

    void Zoom(float scroll)
    {
        Vector3 direction = (transform.position - rotateCenter).normalized;
        float currentDistance = Vector3.Distance(transform.position, rotateCenter);
        float targetDistance = Mathf.Clamp(currentDistance - scroll * zoomSpeed, minDistance, maxDistance);
        transform.position = rotateCenter + direction * targetDistance;
    }

    IEnumerator RotateAroundPoint()
    {
        isRotating = true;
        float rotated = 0f;
        while (rotated < rotateAngle)
        {
            float step = (Time.deltaTime / duration) * rotateAngle;
            if (rotated + step > rotateAngle)
                step = rotateAngle - rotated;
            transform.RotateAround(rotateCenter, rotateAxis, step);
            rotated += step;
            yield return null;
        }
        isRotating = false;
    }
}
