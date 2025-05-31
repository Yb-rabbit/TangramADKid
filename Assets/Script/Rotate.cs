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

    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(RotateAroundPoint());
        }
    }

    IEnumerator RotateAroundPoint()
    {
        isRotating = true;
        float rotated = 0f;
        //float lastAngle = 0f;
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
