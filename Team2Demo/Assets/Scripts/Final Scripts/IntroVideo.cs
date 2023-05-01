using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Camera mainCamera;

    void Start()
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoFinished;

    }

    void OnVideoFinished(VideoPlayer vp)
    {
        //mainCamera.enabled = true;
        //gameObject.SetActive(false);
        ScenesManagerSingleton.Instance.LoadMainMenuScene();
        
    }
}
