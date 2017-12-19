using System;
using UnityEngine;

public class PoseStatus
{
    public const string KEY_CHANGED = "Key changed";
    public const string POSE_DONE = "done!";

    private const int MAX_ACCURACY = 7;
    private const int PENALTY = 3;

    public Pose pose;

    private Animator character;

    private float accuracy = 0;

    private string currentKey = "";
    private int currentIndex = -1;

    public PoseStatus(Pose pose, Animator character)
    {
        this.character = character;
        this.pose = pose;
        SetNextKey();
    }

    public void Next()
    {
        accuracy += pose.isSequence || currentIndex > 0 ? 2 : 1;
        if (pose.idleName != "none") character.SetTrigger(pose.idleName);
        if (accuracy >= MAX_ACCURACY)
        {
            accuracy = MAX_ACCURACY;
            EventManager.TriggerEvent(POSE_DONE);
            character.SetTrigger("Finish");
        }
        if (pose.isSequence || accuracy >= MAX_ACCURACY - 1) SetNextKey();
    }

    public void Reduce(float value)
    {
        if (accuracy > 0 && (pose.isSequence || currentIndex == 0))
        {
            accuracy -= value;
            if (accuracy <= 0) character.SetTrigger("IdleWalk");
        }
    }

    public void Drop()
    {
        accuracy = Math.Max(0, accuracy - PENALTY);
    }

    public void SetNextKey()
    {
        string nextKey;
        if (pose.isSequence)
        {
            do
            {
                nextKey = GetNextKey();
            } while (currentKey == nextKey);
            currentKey = nextKey;
        }
        else
        {
            currentKey = GetNextKey();
        }

        EventManager.TriggerEvent(KEY_CHANGED);
    }

    private string GetNextKey()
    {
        if (pose.isSequence)
        {
            return pose.keys[UnityEngine.Random.Range(0, pose.keys.Length)];
        }
        if (currentIndex < pose.keys.Length - 1)
        {
            return pose.keys[++currentIndex];
        }
        return "";
    }

    public string GetPrevKey()
    {
        return currentIndex > 0 ? pose.keys[currentIndex - 1] : "";
    }

    public string GetCurrentKey()
    {
        return currentKey;
    }

    public float GetProgress()
    {
        return accuracy / MAX_ACCURACY;
    }
}