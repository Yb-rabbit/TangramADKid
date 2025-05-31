using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPass : MonoBehaviour
{
    // 目标场景名称，在Inspector中设置
    public string targetSceneName;

    void Update()
    {
        if (Input.anyKeyDown && !string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
