using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource enemySfxSource;
    [SerializeField] AudioSource enemyMusicSource;

    // Music and SFX Clips
    [SerializeField] AudioClip backGround;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip lossMusic;
    [SerializeField] AudioClip menuMusic;

    public bool isPlaying = false;


    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
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

    // Player One Shot SFX's
    public void PlayCharacterSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Enemy One Shot SFX's
    public void PlayEnemySound(AudioClip clip)
    {
        enemySfxSource.PlayOneShot(clip);
    }

    // Will loop unless dead > Should adjust mixing / 3D settings for better blend and polish
    public void EnemyPatrollingSound(AudioClip clip)
    {
        enemyMusicSource.clip = clip;
        enemyMusicSource.loop = true;
        enemyMusicSource.Play();
    }

    // Background, Win, and Loss Music
    public void PlayBackgroundMusic()
    {
        musicSource.clip = backGround;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayWinMusic()
    {
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = winMusic;

        if (!isPlaying)
        {
            musicSource.Play();
            isPlaying = true;
        }
    }
    public void PlayLossMusic()
    {
        enemySfxSource.Stop();
        enemyMusicSource.Stop();
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = lossMusic;

        if (!isPlaying)
        {
            musicSource.Play();
            isPlaying = true;
        }
    }

    public void PlayMenuMusic()
    {
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = menuMusic;

        if (!isPlaying)
        {
            musicSource.Play();
            isPlaying = true;
        }
    }

}
