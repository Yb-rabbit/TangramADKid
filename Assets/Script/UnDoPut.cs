using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Groundʱ��¼��ȫλ�ã�����Badʱ�ָ�����ȫλ��
/// ��ѡ��ʼ�ջص���ʼλ��
/// </summary>
public class UnDoPut : MonoBehaviour
{
    // ��¼��һ�ΰ�ȫλ��
    private Vector3 lastSafePosition;
    // ��¼��ʼλ��
    private Vector3 initialPosition;

    // �Ƿ�ʼ�ջص���ʼλ�ã�Inspector�ɼ���Ĭ�Ϲرգ�
    public bool resetToInitialPosition = false;

    void Start()
    {
        // ��ʼ��Ϊ��ʼλ��
        initialPosition = transform.position;
        lastSafePosition = initialPosition;
    }

    // ���������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // ����Groundʱ����¼��ǰλ��Ϊ��ȫλ��
            lastSafePosition = transform.position;
        }
        else if (other.CompareTag("Bad"))
        {
            // ����Badʱ���������ûָ�λ��
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
