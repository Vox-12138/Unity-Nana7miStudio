using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControler : MonoBehaviour
{
    // 播放器
    private VideoPlayer video_player;

    // 播放视频的序号
    private int for_num;
    private int unfor_num;

    // 标志播放的曲目的模式
    private bool special_mode = false;
    private bool now_special_mode = false;

    // 视频
    public VideoClip[] forwardable;
    public VideoClip[] unforwardable;

    // BV号
    public string[] forwardable_bv_num;
    public string[] unforwardable_bv_num;

    // 播放按钮
    public GameObject TogglePlayVideo;
    private Toggle toggle_play_video;

    // 列表切换按钮
    public GameObject ToggleSpecialModeOn;
    private Toggle toggle_special_mode_on;

    /// <summary>
    /// 以下是函数
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        // VideoPlayer组件
        video_player =
            this.GetComponent<VideoPlayer>();

        // 设置初始音量
        video_player.SetDirectAudioVolume(trackIndex: 0, 0);

        // 视频播放序号
        for_num = Random.Range(0, forwardable.GetLength(0));
        unfor_num = Random.Range(0, unforwardable.GetLength(0));
        video_player.clip = forwardable[for_num];

        // 播放&暂停键的按钮组件
        toggle_play_video =
            TogglePlayVideo.GetComponent<Toggle>();

        // 播放列表切换键的按钮组件
        toggle_special_mode_on =
            ToggleSpecialModeOn.GetComponent<Toggle>();

        // 每个视频结束时自动播放下一曲
        video_player.loopPointReached += Auto_next;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 调节音量
    public void Set_volume(float volume)
    {
        video_player.SetDirectAudioVolume(trackIndex : 0, volume);
    }

    // 播放&暂停
    public void Video_play_pause()
    {
        if (video_player.isPlaying)
        {
            video_player.Pause();
        }
        else
        {
            video_player.Play();
        }
    }

    // 切换播放列表
    public void Switch_Special_Mode()
    {
        special_mode = toggle_special_mode_on.isOn;
    }

    // 下一曲
    public void Video_next()
    {
        if (special_mode)
        {
            unfor_num += 1;
            if (unfor_num > unforwardable.GetLength(0) - 1)
            {
                unfor_num = 0;
            }

            video_player.clip = unforwardable[unfor_num];

            now_special_mode = true;
        }
        else
        {
            for_num += 1;
            if (for_num >  forwardable.GetLength(0) - 1)
            {
                for_num = 0;
            }

            video_player.clip = forwardable[for_num];

            now_special_mode = false;
        }

        toggle_play_video.isOn = true;
    }

    //上一曲
    public void Video_last()
    {
        if (special_mode)
        {
            unfor_num -= 1;
            if (unfor_num < 0)
            {
                unfor_num = unforwardable.GetLength(0) - 1;
            }

            video_player.clip = unforwardable[unfor_num];

            now_special_mode = true;
        }
        else
        {
            for_num -= 1;
            if (for_num < 0)
            {
                for_num = forwardable.GetLength(0) - 1;
            }

            video_player.clip = forwardable[for_num];

            now_special_mode = false;
        }

        toggle_play_video.isOn = true;
    }

    // 打开网页
    public void Open_web()
    {
        if (now_special_mode)
        {
            Application.OpenURL
                (
                "https://www.bilibili.com/video/" + unforwardable_bv_num[unfor_num]
                );
        }
        else
        {
            Application.OpenURL
               (
               "https://www.bilibili.com/video/" + forwardable_bv_num[for_num]
               );
        }
    }

    // 自动下一曲
    void Auto_next(VideoPlayer vp)
    {
        if (special_mode)
        {
            unfor_num += 1;
            if (unfor_num > unforwardable.GetLength(0) - 1)
            {
                unfor_num = 0;
            }

            vp.clip = unforwardable[unfor_num];

            now_special_mode = true;
        }
        else
        {
            for_num += 1;
            if (for_num > forwardable.GetLength(0) - 1)
            {
                for_num = 0;
            }

            vp.clip = forwardable[for_num];

            now_special_mode = false;
        }

        toggle_play_video.isOn = true;
    }

    // 打开B站自动暂停
    public void Auto_pause()
    {
        toggle_play_video.isOn = false;

        video_player.Pause();
    }
}
