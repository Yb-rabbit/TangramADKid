using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // 需要控制的物体

    // 通过按钮调用此方法
    public void Toggle()
    {
        // 切换目标物体的激活状态
        targetObject.SetActive(!targetObject.activeSelf);
    }
}