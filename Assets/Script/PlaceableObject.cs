using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlaceableObject : MonoBehaviour
{
    public float moveY = 0f; // 拖拽时物体与地面高度差
    public float rotateDuration = 0.3f;

    private static PlaceableObject currentSelected = null;
    private bool isSelected = false;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private Vector3 mouseOffset;

    private int rotateStep = 0;
    private const int totalSteps = 8;
    private bool isFront = true;

    private Quaternion targetRotation;
    private bool isRotating = false;
    private float rotateTimer = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
        targetRotation = transform.rotation;

        float x = Mathf.Round(transform.rotation.eulerAngles.x);
        isFront = Mathf.Approximately(x, 270f) || Mathf.Approximately(x, -90f);
    }

    void Update()
    {
        HandleMouseInput();
        HandleDrag();
        HandleRotation();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            int groundLayer = LayerMask.GetMask("Ground");
            Collider col = GetComponent<Collider>();

            if (!isSelected)
            {
                // 仅当鼠标点到自己时选中
                if (col != null && col.Raycast(ray, out RaycastHit hitInfo, 100f))
                {
                    if (currentSelected != null && currentSelected != this)
                        currentSelected.Deselect();

                    isSelected = true;
                    currentSelected = this;
                    originalPosition = transform.position;

                    // 计算鼠标与物体中心的偏移
                    if (Physics.Raycast(ray, out RaycastHit groundHit, 100f, groundLayer))
                        mouseOffset = transform.position - groundHit.point;
                    else
                        mouseOffset = Vector3.zero;
                }
            }
            else
            {
                // 放下逻辑：点击空白或地面或自己都可放下
                TryPlace();
            }
        }
    }

    private void HandleDrag()
    {
        if (!isSelected) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        int groundLayer = LayerMask.GetMask("Ground");
        if (Physics.Raycast(ray, out RaycastHit groundHit, 100f, groundLayer))
        {
            Vector3 targetPos = groundHit.point + mouseOffset;
            transform.position = new Vector3(targetPos.x, groundHit.point.y + moveY, targetPos.z);
        }
    }

    private void HandleRotation()
    {
        if (!isSelected) return;

        if (Input.GetKeyDown(KeyCode.T) && !isRotating)
        {
            rotateStep = (rotateStep + 1) % totalSteps;
            float yAngle = 45f * rotateStep;
            float xAngle = isFront ? -90f : 90f;
            targetRotation = Quaternion.Euler(xAngle, yAngle, 0);
            isRotating = true;
            rotateTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Y) && !isRotating)
        {
            isFront = !isFront;
            float yAngle = 45f * rotateStep;
            float xAngle = isFront ? -90f : 90f;
            targetRotation = Quaternion.Euler(xAngle, yAngle, 0);
            isRotating = true;
            rotateTimer = 0f;
        }

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

    private void TryPlace()
    {
        if (IsValidPlacement())
        {
            isSelected = false;
            currentSelected = null;
            // 保持当前位置
        }
        else
        {
            isSelected = false;
            currentSelected = null;
            transform.position = originalPosition;
            Debug.Log("当前位置无法放置！");
        }
    }

    private bool IsValidPlacement()
    {
        Collider col = GetComponent<Collider>();
        if (col == null) return true;

        Bounds bounds = col.bounds;
        // 只排除 Ignore Raycast 层
        int ignoreLayer = LayerMask.GetMask("Ignore Raycast");
        Collider[] hits = Physics.OverlapBox(bounds.center, bounds.extents * 0.95f, transform.rotation, ~ignoreLayer, QueryTriggerInteraction.Ignore);
        foreach (var hit in hits)
        {
            if (hit.transform == transform) continue;
            if (hit.gameObject.layer == LayerMask.NameToLayer("Ground")) continue;
            return false;
        }

        // 地面检测
        Vector3 bottomCenter = new Vector3(bounds.center.x, bounds.min.y + 0.01f, bounds.center.z);
        float rayLength = 2f;
        int groundLayer = LayerMask.GetMask("Ground");
        if (!Physics.Raycast(bottomCenter, Vector3.down, rayLength, groundLayer))
        {
            Debug.DrawRay(bottomCenter, Vector3.down * rayLength, Color.red, 1f);
            Debug.LogWarning("地面检测失败，Layer是否为Ground？底部点：" + bottomCenter);
            return false;
        }

        return true;
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
