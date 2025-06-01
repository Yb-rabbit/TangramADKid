using UnityEngine;
using UnityEngine.Events;

public class SimulateKeyPress : MonoBehaviour
{
    public KeyCode keyY = KeyCode.Y; // 模拟按下的Y键
    public KeyCode keyT = KeyCode.T; // 模拟按下的T键

    public UnityEvent onKeyPressY; // 按下Y键时触发的事件
    public UnityEvent onKeyPressT; // 按下T键时触发的事件

    public void OnButtonClick()
    {
        // 模拟按下Y键
        onKeyPressY.Invoke();

        // 模拟按下T键
        onKeyPressT.Invoke();
    }
}