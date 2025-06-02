using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 通过UI按钮切换场景的脚本
/// </summary>
public class UIButtonSceneLoader : MonoBehaviour
{
    /// <summary>
    /// 切换到指定名称的场景（可在按钮OnClick事件中设置参数）
    /// </summary>
    /// <param name="sceneName">目标场景名称</param>
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("场景名称不能为空！");
        }
    }
}
