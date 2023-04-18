using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private AudioClip backGround;
    private AudioClip dash;
    private AudioClip emp;
    private AudioClip invisOn;
    private AudioClip invisOff;
    private AudioClip jump;
    private AudioClip healthDown;
    private AudioClip healthUp;
    private AudioClip enemyBacground;
    private AudioClip enemyDown;
    private AudioClip enemyOut;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
          
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }


}
