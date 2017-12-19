using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : Window
{
    public enum Reason
    {
        FailedPose, BadMood
    }

    private const string FAILED_POSE_TEXT = "The picture was unfinished and didn't go viral.";
    private const string BAD_MOOD_TEXT = "You wasn't in a good mood this time and the picture doesn't come together. ";

    private Text likes;
    private Text followers;
    private Text failText;
    private Button close;
    private Image photo;

    private int likesMax;
    private int likesCurrent;
    private int followersMax;
    private int followersCurrent;
    private string followersSign;

    private bool isRunning;

    void Start()
    {
        likes = transform.Find("LikesValue").GetComponent<Text>();
        followers = transform.Find("FollowersValue").GetComponent<Text>();
        failText = transform.Find("FailReason").GetComponent<Text>();
        close = transform.Find("Close").GetComponent<Button>();

        close.onClick.AddListener(Exit);
    }

    public void ShowImage(string image)
    {
        UIUtils.LoadSourceToImage(transform.Find("Image").GetComponent<Image>(), "Poses/" + image);
    }

    public void ShowStats(int likesValue, int followersValue)
    {
        likesMax = likesValue;
        followersMax = Math.Abs(followersValue);
        followersSign = followersValue > 0 ? "+" : "-";
        isRunning = true;
    }

    public void HideFailMessage()
    {
        failText.gameObject.SetActive(false);
    }

    public void ShowFail(Reason reason)
    {
        transform.Find("LikesLabel").gameObject.SetActive(false);
        likes.gameObject.SetActive(false);

        failText.text = reason == Reason.FailedPose ? FAILED_POSE_TEXT : BAD_MOOD_TEXT;
    }

    void Exit()
    {
        EventManager.TriggerEvent(InstagramMinigame.EXIT_GAME);
    }

    void FixedUpdate()
    {
        if (isRunning)
        {
            if (followersSign == "+")
            {
                if (likesCurrent < likesMax) likes.text = (++likesCurrent).ToString();
            }
            if (followersCurrent < followersMax) followers.text = followersSign + (++followersCurrent).ToString();
            isRunning = (likesCurrent < likesMax || followersCurrent < followersMax);
        }
    }

    public override void Hide()
    {
        base.Hide();
        Exit();
    }
}