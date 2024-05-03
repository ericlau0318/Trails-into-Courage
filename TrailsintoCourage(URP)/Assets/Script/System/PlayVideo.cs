using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public GameObject loadingPanel;
    public GameObject canvas, button;
    public static bool first;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        button.SetActive(false);
    }
    private void Update()
    {
        if(!loadingPanel.activeSelf && first)
        {
            videoPlayer.Play();
            canvas.SetActive(false);
            button.SetActive(true);
            first = false;
            videoPlayer.loopPointReached += OnVideoComplete;
        }
    }

    private void OnVideoComplete(VideoPlayer vp)
    {
        Debug.Log("Video completed!");
        canvas.SetActive(true);
        videoPlayer.Stop();
    }

    public void SkipVideo()
    {
        videoPlayer.Stop();
        canvas.SetActive(true);
        button.SetActive(false);
    }

}
