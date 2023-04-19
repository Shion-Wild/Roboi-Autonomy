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

    public void PlayBackgroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayCharacterSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Will loop unless dead > Should adjust mixing / 3D settings for better blend and polish
    public void EnemyPatrollingSound(AudioClip clip)
    {
        enemyMusicSource.clip = clip;
        enemyMusicSource.loop = true;
        enemyMusicSource.Play();
    }

    public void PlayEnemySound(AudioClip clip)
    {
        enemySfxSource.PlayOneShot(clip);
    }






}
