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
