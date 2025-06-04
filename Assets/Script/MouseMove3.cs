using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove3 : MonoBehaviour
{
    public float moveY = 2f;
    public Camera targetCamera; // ����Inspector��ָ�������

    private static MouseMove3 currentSelected = null;

    private bool isSelected = false;
    private Vector3 originalPosition;
    private Vector3 mouseOffset;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
        originalPosition = transform.position;

        // ���ó�ʼ��תΪ(0,0,0)
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (targetCamera == null) return;

        if (!isSelected)
        {
            // ֻ��δѡ��ʱ���ż����ѡ��
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
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
                        // ��קƽ����������ߴ�ֱ
                        Plane plane = new Plane(targetCamera.transform.forward, transform.position);
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
                Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(targetCamera.transform.forward, transform.position);
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Vector3 targetPos = hitPoint - mouseOffset;
                    // ����Y��߶Ȳ���
                    transform.position = new Vector3(targetPos.x, originalPosition.y + moveY, targetPos.z);
                }
            }
        }
    }

    public void Deselect()
    {
        isSelected = false;
        // ���޸�transform.position
    }
}
