using UnityEngine;
using UnityEngine.Events;

public class SimulateKeyPress : MonoBehaviour
{
    public KeyCode keyY = KeyCode.Y; // ģ�ⰴ�µ�Y��
    public KeyCode keyT = KeyCode.T; // ģ�ⰴ�µ�T��

    public UnityEvent onKeyPressY; // ����Y��ʱ�������¼�
    public UnityEvent onKeyPressT; // ����T��ʱ�������¼�

    public void OnButtonClick()
    {
        // ģ�ⰴ��Y��
        onKeyPressY.Invoke();

        // ģ�ⰴ��T��
        onKeyPressT.Invoke();
    }
}