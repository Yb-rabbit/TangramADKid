using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进入Ground时记录安全位置，进入Bad时恢复到安全位置
/// 可选：始终回到初始位置
/// </summary>
public class UnDoPut : MonoBehaviour
{
    // 记录上一次安全位置
    private Vector3 lastSafePosition;
    // 记录初始位置
    private Vector3 initialPosition;

    // 是否始终回到初始位置（Inspector可见，默认关闭）
    public bool resetToInitialPosition = false;

    void Start()
    {
        // 初始化为起始位置
        initialPosition = transform.position;
        lastSafePosition = initialPosition;
    }

    // 触发器检测
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // 进入Ground时，记录当前位置为安全位置
            lastSafePosition = transform.position;
        }
        else if (other.CompareTag("Bad"))
        {
            // 进入Bad时，根据设置恢复位置
            if (resetToInitialPosition)
            {
                transform.position = initialPosition;
            }
            else
            {
                transform.position = lastSafePosition;
            }
        }
    }
}
