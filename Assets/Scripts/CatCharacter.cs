using System;
using System.Collections;
using System.Collections.Generic;

public class CatCharacter
{
    public enum Jobs
    {
        Instagram
    }

    private const int MAX_EFFECTS = 2;

    public static List<Attribute> attributes = new List<Attribute>();
    public static List<MoodletEffect> effects = new List<MoodletEffect>();

    public static Jobs currentQuest;
    public static bool hasActiveJob;
    public static Achievement[] achievements = Achievement.all;
    public static int likes = 0;
    public static int followers = 0;
    public static int lastLikes = 0;
    public static int lastFollowers = 0;
    public static string bestPose = "Simple";
    public static float bestPoseScore = 0;

    void Init()
    {
        FillAttributes(attributes);
    }

    private static void FillAttributes(List<Attribute> list)
    {
        if (list.Count == 0)
        {
            list.Add(new Attribute(Attribute.Type.Playfullness, 0));
            list.Add(new Attribute(Attribute.Type.Strength, 0));
            list.Add(new Attribute(Attribute.Type.Lazyness, 0));
            list.Add(new Attribute(Attribute.Type.Toughness, 0));
            list.Add(new Attribute(Attribute.Type.Curiosity, 0));
            list.Add(new Attribute(Attribute.Type.Sociality, 0));
        }
    }

    public static void AddMoodlet(MoodletEffect effect)
    {
        if (effects.IndexOf(effect) < 0)
        {
            effects.Add(effect);
            if (effects.Count > MAX_EFFECTS) effects.RemoveAt(0);
            EventManager.TriggerEvent(MoodletEffect.EFFECT_ADDED);
        }
    }

    public static void AddLikes(int newLikes)
    {
        lastLikes = newLikes;
        likes += newLikes;

        UpdateAchievements(achievements, Achievement.KeyStat.Like, likes);
        UpdateAchievements(achievements, Achievement.KeyStat.OneTimeLike, newLikes);
    }

    public static void AddFollowers(int newFollowers)
    {
        lastFollowers = newFollowers;
        followers += newFollowers;

        UpdateAchievements(achievements, Achievement.KeyStat.Follower, followers);
        UpdateAchievements(achievements, Achievement.KeyStat.OneTimeFollower, newFollowers);
    }

    private static void UpdateAchievements(Achievement[] list, Achievement.KeyStat key, int value)
    {
        foreach (Achievement data in list)
        {
            if (data.key == key && data.value < value) data.value = Math.Min(data.max, value);
        }
    }

    public static void IncreasePoses(Achievement.KeyStat pose)
    {
        foreach (Achievement data in achievements)
        {
            if (data.key == pose) data.value = Math.Min(data.max, data.value + 1);
        }
    }
}