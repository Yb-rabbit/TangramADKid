using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove3 : MonoBehaviour
{
    public float moveY = 2f;
    public Camera targetCamera; // 可在Inspector中指定摄像机

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

        // 设置初始旋转为(0,0,0)
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        if (targetCamera == null) return;

        if (!isSelected)
        {
            // 只有未选中时，才检测点击选中
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
                        // 拖拽平面与相机视线垂直
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
            // 拖拽中，鼠标左键再次按下则放下
            if (Input.GetMouseButtonDown(0))
            {
                isSelected = false;
                currentSelected = null;
                transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z);
            }
            else
            {
                // 拖拽跟随鼠标
                Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(targetCamera.transform.forward, transform.position);
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Vector3 targetPos = hitPoint - mouseOffset;
                    // 保持Y轴高度不变
                    transform.position = new Vector3(targetPos.x, originalPosition.y + moveY, targetPos.z);
                }
            }
        }
    }

    public void Deselect()
    {
        isSelected = false;
        // 不修改transform.position
    }
}
