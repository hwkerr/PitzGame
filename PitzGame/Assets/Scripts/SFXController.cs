using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    public AudioSource m_AudioSource;

    public AudioClip swordAttack;
    public AudioClip jump;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = swordAttack;
        ;// swordAttack = GetComponent
    }

    public void PlaySwordAttack()
    {
        m_AudioSource.clip = swordAttack;
        m_AudioSource.Play();
    }

    public void PlayJump()
    {
        m_AudioSource.clip = jump;
        m_AudioSource.Play();
    }
}
