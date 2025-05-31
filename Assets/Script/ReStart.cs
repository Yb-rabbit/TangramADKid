using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    // 可以在Inspector中指定按键，默认为R
    public KeyCode resetKey = KeyCode.R;

    // 需要取消激活的物体组
    public GameObject[] objectsToDeactivate;

    void Update()
    {
        if (Input.GetKeyDown(resetKey))
        {
            // 取消激活指定物体组
            if (objectsToDeactivate != null)
            {
                foreach (var obj in objectsToDeactivate)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }
            // 重新加载当前场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
