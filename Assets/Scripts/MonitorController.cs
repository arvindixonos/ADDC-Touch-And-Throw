using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[System.Serializable]
public class VideoData
{
    public string id;
    public VideoClip leftScreenClip;
    public VideoClip centerScreenClip;
    public VideoClip rightScreenClip;
}
public class MonitorController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer leftScreenPlayer;
    [SerializeField]
    private VideoPlayer centerScreenPlayer;
    [SerializeField]
    private VideoPlayer rightScreenPlayer;
    [SerializeField]
    private VideoData[] videoDatas;

    private VideoData currentVideoData;
    void Start()
    {
        Init();
    }
    private void Init()
    {
        leftScreenPlayer.Stop();
        centerScreenPlayer.Stop();
        rightScreenPlayer.Stop();

        leftScreenPlayer.isLooping = true;
        centerScreenPlayer.isLooping = true;
        rightScreenPlayer.isLooping = true;
    }
    private void OnMessageReceived(string message)
    {
        PlayVideo(message);
    }
    private void PlayVideo(string id)
    {
        currentVideoData = GetVideoData(id);

        leftScreenPlayer.Stop();
        centerScreenPlayer.Stop();
        rightScreenPlayer.Stop();

        if (currentVideoData.leftScreenClip)
        {
            leftScreenPlayer.clip = currentVideoData.leftScreenClip;
            leftScreenPlayer.Play();
        }
        if (currentVideoData.centerScreenClip)
        {
            centerScreenPlayer.clip = currentVideoData.centerScreenClip;
            centerScreenPlayer.Play();
        }
        if (currentVideoData.rightScreenClip)
        {
            rightScreenPlayer.clip = currentVideoData.rightScreenClip;
            rightScreenPlayer.Play();
        }

    }
    private VideoData GetVideoData(string id)
    {
        for (int i = 0; i < videoDatas.Length; i++)
        {
            if (id == videoDatas[i].id)
            {
                return currentVideoData = videoDatas[i];
            }
        }
        return null;
    }
}
