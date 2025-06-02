using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ͨ��UI��ť�л������Ľű�
/// </summary>
public class UIButtonSceneLoader : MonoBehaviour
{
    /// <summary>
    /// �л���ָ�����Ƶĳ��������ڰ�ťOnClick�¼������ò�����
    /// </summary>
    /// <param name="sceneName">Ŀ�곡������</param>
    public void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("�������Ʋ���Ϊ�գ�");
        }
    }
}
