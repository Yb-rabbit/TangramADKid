using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // 可在UI按钮的OnClick事件中绑定此方法
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // 在编辑器下用于测试
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
