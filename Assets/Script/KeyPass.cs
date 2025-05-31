using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPass : MonoBehaviour
{
    // Ŀ�곡�����ƣ���Inspector������
    public string targetSceneName;

    void Update()
    {
        if (Input.anyKeyDown && !string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
