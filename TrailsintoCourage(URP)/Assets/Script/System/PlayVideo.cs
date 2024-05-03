using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public GameObject LoadingScreen;
    public static bool first;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if(!LoadingScreen.activeSelf && first)
        {
            videoPlayer.Play();
            first = false;
            videoPlayer.loopPointReached += OnVideoComplete;
        }
    }

    private void OnVideoComplete(VideoPlayer vp)
    {
        Debug.Log("Video completed!");
        videoPlayer.Stop();
    }

}
