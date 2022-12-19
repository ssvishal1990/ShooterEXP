using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{

    [SerializeField] AudioSource m_AudioSource;

    [SerializeField] AudioClip mainWeaponAudioClip;
    [SerializeField] AudioClip specialWeaponAudioClip;
    
    // Start is called before the first frame update
    void Start()
    {
        Combat.onFireMainWeapon += Combat_OnFireMainWeapon;    
    }

    private void Combat_OnFireMainWeapon(object sender, Vector3 position)
    {
        m_AudioSource.PlayOneShot(mainWeaponAudioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMainWeaponFireSound()
    {
        m_AudioSource.PlayOneShot(mainWeaponAudioClip);
    }

    public void playSpecialWeaponFireSound()
    {
        m_AudioSource.clip = specialWeaponAudioClip;
        m_AudioSource.time = 3f;
        m_AudioSource.Play();
    }
}
