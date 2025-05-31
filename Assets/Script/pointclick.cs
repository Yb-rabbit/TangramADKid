using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pointclick : MonoBehaviour
{
    // ��Inspector������AudioSource������Start��GetComponent<AudioSource>()
    public AudioSource audioSource;

    void Start()
    {
        // ���δ��Inspectorָ�������Զ���ȡ
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && audioSource != null)
        {
            audioSource.Play();
        }
    }
}
