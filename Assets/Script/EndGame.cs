using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // ����UI��ť��OnClick�¼��а󶨴˷���
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // �ڱ༭�������ڲ���
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
