using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShootingView : MonoBehaviour
{
    public const string END_SHOT = "end shot";

    private const int SHOT_MAX_TIME = 8;

    public Animator character;

    [SerializeField]
    private InstagramSounds sounds;

    private Transform shootingUI;
    private GameObject crosshair;
    private Image flash;
    private Slider shotTime;

    private bool isPreparing;

    void Start()
    {
        shootingUI = transform.GetChild(0);
        crosshair = shootingUI.Find("Camera").gameObject;
        flash = shootingUI.Find("Flash").gameObject.GetComponent<Image>();
        shotTime = shootingUI.Find("ShotTime").gameObject.GetComponent<Slider>();

        shotTime.value = 0;

        shotTime.maxValue = SHOT_MAX_TIME;
    }

    public void Pause(bool value)
    {
        isPreparing = !value;
    }

    void Update()
    {
        if (isPreparing)
        {
            UpdateShotTime();
        }
    }

    private void UpdateShotTime()
    {
        shotTime.value = Math.Min(SHOT_MAX_TIME, shotTime.value + Time.deltaTime);
        if (shotTime.value == SHOT_MAX_TIME)
        {
            sounds.PlayLose();
            MakeShot();
        }
    }

    public void MakeShot()
    {
        sounds.StopBG();
        shotTime.value = 0;
        isPreparing = false;
        FlashCamera(0.3f);
    }

    private void FlashCamera(float time)
    {
        sounds.PlayCamera();
        DOTween.To(() => flash.color, value => flash.color = value, Color.white, time)
            .SetRelative()
            .SetLoops(2, LoopType.Yoyo);
        Invoke("EndShot", 0.6f);
    }

    private void EndShot()
    {
        shootingUI.gameObject.SetActive(false);
        EventManager.TriggerEvent(END_SHOT);
    }

    public void Quit()
    {
        sounds.StopBG();
        sounds.PlayWin();
        isPreparing = false;
        shootingUI.gameObject.SetActive(false);
        character.SetTrigger("IdleSit");
        Invoke("ChangeScene", 3.5f);
    }

    void ChangeScene()
    {
        EventManager.TriggerEvent(InstagramMinigame.EXIT_GAME);
    }
}