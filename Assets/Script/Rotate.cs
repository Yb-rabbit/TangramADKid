using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // 旋转中心点
    public Vector3 rotateCenter = Vector3.zero;
    // 旋转轴
    public Vector3 rotateAxis = Vector3.up;
    // 旋转角度
    public float rotateAngle = 90f;
    // 旋转持续时间（秒）
    public float duration = 0.5f;

    // 调焦参数
    public float minDistance = 2f;   // 最小距离
    public float maxDistance = 20f;  // 最大距离
    public float zoomSpeed = 5f;     // 缩放速度

    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(RotateAroundPoint());
        }

        // 鼠标滚轮调焦
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
