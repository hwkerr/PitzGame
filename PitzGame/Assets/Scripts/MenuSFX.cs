using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFX : MonoBehaviour {

    private AudioSource m_AudioSource;

    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip deselect;
    
    void Start () {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySelection()
    {
        m_AudioSource.volume = 0.8f;
        m_AudioSource.clip = select;
        m_AudioSource.Play();
    }

    public void PlayDeselection()
    {
        m_AudioSource.volume = 0.1f;
        m_AudioSource.clip = deselect;
        m_AudioSource.Play();
    }
}
