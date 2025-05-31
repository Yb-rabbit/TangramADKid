using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    // ������Inspector��ָ��������Ĭ��ΪR
    public KeyCode resetKey = KeyCode.R;

    // ��Ҫȡ�������������
    public GameObject[] objectsToDeactivate;

    void Update()
    {
        if (Input.GetKeyDown(resetKey))
        {
            // ȡ������ָ��������
            if (objectsToDeactivate != null)
            {
                foreach (var obj in objectsToDeactivate)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }
            // ���¼��ص�ǰ����
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
