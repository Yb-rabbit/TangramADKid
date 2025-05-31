using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pointclick : MonoBehaviour
{
    // 在Inspector中拖入AudioSource，或在Start中GetComponent<AudioSource>()
    public AudioSource audioSource;

    void Start()
    {
        // 如果未在Inspector指定，则自动获取
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
