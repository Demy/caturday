using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InstagramKeyboardController))]
public class InstagramMinigame : MonoBehaviour
{
    public static string EXIT_GAME = "exit";

    [SerializeField]
    private Animator character;
    [SerializeField]
    private PoseWanted wantedView;
    [SerializeField]
    private PoseKeySquenceView keyPose;
    [SerializeField]
    private PoseKeySquenceView strugglePose;
    [SerializeField]
    private Slider poseAccuracy;
    [SerializeField]
    private Slider struggleAccuracy;
    [SerializeField]
    private InstagramSounds sounds;
    [SerializeField]
    private ShootingView shootingView;

    [SerializeField]
    private ResultWindow result;
    [SerializeField]
    private HelpHint hint;

    private Pose pose;
    private PoseStatus status;
    private PoseStatus struggleStatus;
    private int efficiency;
    private int resultState;
    private bool notStopped;
    private bool isPosing;
    private bool isStruggling;

    void Start()
    {
        result.Hide();

        pose = Pose.poses[Random.Range(0, Pose.poses.Length)];
        status = new PoseStatus(pose, character);
        struggleStatus = new PoseStatus(Pose.Struggle, character);
        CalculateEfficiency();

        SetWantedPose();
        SetKeyPose();
        SetStrugglePose();

        notStopped = true;

        if (CatCharacter.followers == 0)
        {
            ShowHint();
        }
        else
        {
            hint.Hide();
            StartTimer();
        }

        EventManager.StartListening(PoseStatus.POSE_DONE, OnPoseFinished);
        EventManager.StartListening(ShootingView.END_SHOT, OnEndShot);
        EventManager.StartListening(EXIT_GAME, LoadNextScene);
    }

    private void ShowHint()
    {
        notStopped = false;
        shootingView.Pause(true);

        EventManager.StartListening(HelpHint.CLOSED, StartTimer);
    }

    private void StartTimer()
    {
        EventManager.StopListening(HelpHint.CLOSED, StartTimer);
        shootingView.Pause(false);
        notStopped = true;
    }

    private void CalculateEfficiency()
    {
        efficiency = CalculateAttributes(pose.positiveAttributes, 1);
        efficiency += CalculateAttributes(pose.negativeAttributes, -1);
    }

    private int CalculateAttributes(Attribute.Type[] attributeTypes, int sign)
    {
        int result = 0;
        foreach (Attribute.Type type in attributeTypes)
        {
            result += GetAtribute(type) * sign;
        }
        return result;
    }

    private int GetAtribute(Attribute.Type type)
    {
        int result = 0;
        foreach (Attribute attribute in CatCharacter.attributes)
        {
            if (attribute.type == type) result += attribute.value;
        }
        foreach (MoodletEffect mood in CatCharacter.effects)
        {
            foreach (Attribute effect in mood.effects)
            {
                if (effect.type == type) result += effect.value;
            }
        }
        return result;
    }

    private void SetWantedPose()
    {
        wantedView.SetPose(pose);
        wantedView.SetEfficiency(efficiency);
    }

    private void SetKeyPose()
    {
        keyPose.SetPose(status);
    }

    private void SetStrugglePose()
    {
        strugglePose.SetPose(struggleStatus);
    }

    public void MakePose(string key)
    {
        if (status.GetCurrentKey() == key)
        {
            if (!isPosing)
            {
                keyPose.transform.DOScale(new Vector3(-0.1f, -0.1f), 0.2f).SetRelative().
                    SetLoops(-1, LoopType.Yoyo);
            }
            isStruggling = false;
            isPosing = true;

            status.Next();
            struggleStatus.Drop();
        }
        else if (struggleStatus.GetCurrentKey() == key)
        {
            keyPose.transform.DOKill();
            isPosing = false;
            isStruggling = true;

            struggleStatus.Next();
            status.Drop();
        }
        else if (status.GetPrevKey() != key)
        {
            keyPose.transform.DOKill();
            isStruggling = false;
            isPosing = false;

            status.Drop();
            struggleStatus.Drop();
        }
        SetProgress();
    }

    private void SetProgress()
    {
        poseAccuracy.value = status.GetProgress();
        struggleAccuracy.value = struggleStatus.GetProgress();
    }

    void FixedUpdate()
    {
        if (notStopped)
        {
            status.Reduce(Time.deltaTime);
            struggleStatus.Reduce(Time.deltaTime);
            SetProgress();
        }
    }

    private void OnEndShot()
    {
        string icon = GetIconByProgress(resultState);
        if (CatCharacter.bestPoseScore <= status.GetProgress())
        {
            CatCharacter.bestPose = icon;
            CatCharacter.bestPoseScore = status.GetProgress();
        }
        result.Show();
        result.ShowImage(icon);
        result.ShowStats(CatCharacter.lastLikes, CatCharacter.lastFollowers);
        if (CatCharacter.lastFollowers <= 0)
        {
            result.ShowFail(status.GetProgress() < 1f ?
                ResultWindow.Reason.FailedPose :
                ResultWindow.Reason.BadMood);
        }
        else
        {
            result.HideFailMessage();
        }
    }

    private string GetIconByProgress(int state)
    {
        string result = pose.icon;
        if (state < 0 || status.GetProgress() == 0) result = "Simple";
        else if (state == 0) result += "Fail";
        else if (state == 1 && (Random.value < 0.65f)) result += "Try";
        return result;
    }

    private void OnPoseFinished()
    {
        notStopped = false;

        if (status.GetProgress() > struggleStatus.GetProgress())
        {
            Invoke("MakeShot", 2f);
            sounds.PlayWin();
        }
        else
        {
            shootingView.Quit();
        }
    }

    private void MakeShot()
    {
        if (status.GetProgress() == 1f) CatCharacter.IncreasePoses(pose.achieveKey);
        AddLikesAndFollowers();
        shootingView.MakeShot();
    }

    private void AddLikesAndFollowers()
    {
        EvaluateState(status.GetProgress() > 0.999f, efficiency);

        int value = 25 + (int)Math.Round(CatCharacter.followers * 0.05) + Random.Range(1, 9);
        int followers = 0;
        if (resultState == 2) followers = (int) Math.Round(value * 1.5);
        if (resultState == 1) followers = value;
        if (resultState == 0) followers = Random.Range(-12, 12);
        if (resultState == -1) followers = -value;
        if (resultState == -2) followers = -(int) Math.Round(value * 1.5);

        if (followers < 0 && Math.Abs(followers) > CatCharacter.followers)
            followers = -CatCharacter.followers;

        CatCharacter.AddLikes(Math.Max(0,
            followers + (int) Math.Round(Random.Range(0.15f, 0.35f) * CatCharacter.followers)));
        CatCharacter.AddFollowers(followers);
    }

    private void EvaluateState(bool isFullPose, int efficiency)
    {
        if (isFullPose)
        {
            if (efficiency > 0 && efficiency < 2)
            {
                resultState = 1;
            }
            else if (efficiency >= 2)
            {
                resultState = 2;
            }
            else if (efficiency == 0)
            {
                resultState = 0;
            }
            else if (efficiency > -2)
            {
                resultState = -1;
            }
            else
            {
                resultState = -2;
            }
        }
        else
        {
            resultState = -2;
        }
    }

    void LoadNextScene()
    {
        if (struggleStatus.GetProgress() == 1) CatCharacter.IncreasePoses(Pose.Struggle.achieveKey);
        CatCharacter.effects.Clear();
        SceneManager.LoadScene("Default");
    }

    void OnDestroy()
    {
        EventManager.StopListening(PoseStatus.POSE_DONE, OnPoseFinished);
        EventManager.StopListening(ShootingView.END_SHOT, OnEndShot);
        EventManager.StopListening(EXIT_GAME, LoadNextScene);
    }

    public bool IsNotStopped()
    {
        return notStopped;
    }
}