using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float moveY = 2f;

    private static MouseMove currentSelected = null; // ��̬��������¼��ǰѡ�е�����

    private bool isSelected = false;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private Vector3 mouseOffset;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    if (!isSelected)
                    {
                        // ȡ��֮ǰѡ�е�����
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
                    else
                    {
                        isSelected = false;
                        currentSelected = null;
                        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
                    }
                }
            }
        }

        if (isSelected)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, originalPosition.y + moveY, 0));
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 targetPos = hitPoint - mouseOffset;
                transform.position = new Vector3(targetPos.x, originalPosition.y + moveY, targetPos.z);
            }
        }
    }

    // ȡ��ѡ�в��ָ�yֵ
    public void Deselect()
    {
        isSelected = false;
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
    }
}
