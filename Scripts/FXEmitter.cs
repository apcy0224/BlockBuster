using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXEmitter : MonoBehaviour {
    private AudioSource m_audioSource = null;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();    
    }

    public void Play()
    {
        if(m_audioSource.clip != null)
        {
            m_audioSource.Play();
            Destroy(this.gameObject, m_audioSource.clip.length);
        }
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        if(m_audioSource == null) { m_audioSource = GetComponent<AudioSource>(); }
        m_audioSource.clip = audioClip;
    }
}
