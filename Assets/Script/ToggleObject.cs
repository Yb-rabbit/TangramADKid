using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // ��Ҫ���Ƶ�����

    // ͨ����ť���ô˷���
    public void Toggle()
    {
        // �л�Ŀ������ļ���״̬
        targetObject.SetActive(!targetObject.activeSelf);
    }
}