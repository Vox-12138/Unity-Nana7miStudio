using Live2D.Cubism.Framework;
using Live2D.Cubism.Framework.LookAt;
using Live2D.Cubism.Framework.MouthMovement;
using UnityEngine;
using UnityEngine.UI;

public class CenterControler : MonoBehaviour
{
    private Animator animator;
    private CubismEyeBlinkController eye_blink_controler;
    private CubismLookController look_controler;
    private CubismMouthController mouth_controller;
    private AudioSource audio_source;

    private int motion_num;
    private int un_motion_num;
    private int gn_motion_num;

    private bool special_mode;

    // 容纳动画音源
    public AudioClip[] audio_clips;
    public AudioClip[] un_audio_clips;
    public AudioClip[] gn_audio_clips;

    // 列表切换按钮
    public GameObject ToggleSpecialModeOn;
    private Toggle toggle_special_mode_on;

    /// <summary>
    /// 以下是函数
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        // 30帧足矣
        Application.targetFrameRate = 30;

        // 动画机
        animator = 
            this.GetComponent<Animator>();

        // 自动眨眼
        eye_blink_controler = 
            this.GetComponent<CubismEyeBlinkController>();

        // 目光跟随
        look_controler = 
            this.GetComponent<CubismLookController>();

        // 口型适配
        mouth_controller =
            this.GetComponent<CubismMouthController>();
        mouth_controller.enabled = false;

        // 播放列表切换键的按钮组件
        toggle_special_mode_on =
            ToggleSpecialModeOn.GetComponent<Toggle>();

        // 音源
        audio_source =
            this.GetComponent<AudioSource>();

        // 动画播放序号
        motion_num = Random.Range(1, audio_clips.Length + 1);
        un_motion_num = Random.Range(1, un_audio_clips.Length + 1);
        gn_motion_num = 1;

        // 切换播放列表
        special_mode = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 播放动画
    public void Motion_play()
    {
        // 序号循环
        if (special_mode)
        {
            un_motion_num += 1;
            if (un_motion_num > un_audio_clips.Length)
            {
                un_motion_num = 1;
            }

            // 动画播放
            animator.SetInteger("un_motion_num", un_motion_num);
        }
        else
        {
            motion_num += 1;
            if (motion_num > audio_clips.Length)
            {
                motion_num = 1;
            }

            // 动画播放
            animator.SetInteger("motion_num", motion_num);
        }
    }

    // 动画开始播放时调用
    public void Motion_start()
    {
        // 关闭自动眨眼和目光跟随，打开口型适配
        eye_blink_controler.enabled = false;
        look_controler.enabled = false;
        mouth_controller.enabled = true;

        // 播放音源
        if (special_mode)
        {
            audio_source.clip = un_audio_clips[un_motion_num - 1];
            audio_source.Play();
        }
        else
        {
            audio_source.clip = audio_clips[motion_num - 1];
            audio_source.Play();
        }

        // 回到idle动画
        animator.SetInteger("motion_num", 0);
        animator.SetInteger("un_motion_num", 0);
        animator.SetInteger("gn_motion_num", 0);
    }

    // 动画结束播放时调用
    public void Motion_end()
    {
        eye_blink_controler.enabled = true;
        look_controler.enabled = true;
        mouth_controller.enabled = false;

        animator.SetInteger("motion_num", 0);
        animator.SetInteger("un_motion_num", 0);
        animator.SetInteger("gn_motion_num", 0);
    }

    // 切换播放列表
    public void Switch_Special_Mode()
    {
        special_mode = toggle_special_mode_on.isOn;

        animator.SetBool("special_mode", special_mode);

    }

    /// <summary>
    /// 以下是GN部分
    /// </summary>

    // 播放GN动画
    public void GN_Motion_play()
    {
        // 序号循环
        if (special_mode)
        {
            gn_motion_num += 1;
            if (gn_motion_num > gn_audio_clips.Length)
            {
                gn_motion_num = 1;
            }

            // 动画播放
            animator.SetInteger("gn_motion_num", gn_motion_num);
        }
    }

    // 动画开始播放时调用
    public void GN_Motion_start()
    {
        // 打开口型适配
        mouth_controller.enabled = true;

        // 播放音源
        audio_source.clip = gn_audio_clips[gn_motion_num - 1];
        audio_source.Play();

        // 回到idle动画
        animator.SetInteger("motion_num", 0);
        animator.SetInteger("un_motion_num", 0);
        animator.SetInteger("gn_motion_num", 0);
    }

    // 动画结束播放时调用
    public void GN_Motion_end()
    {
        mouth_controller.enabled = false;

        animator.SetInteger("motion_num", 0);
        animator.SetInteger("un_motion_num", 0);
        animator.SetInteger("gn_motion_num", 0);
    }

}
