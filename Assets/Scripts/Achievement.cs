using System;

[Serializable]
public class Achievement
{
    public static Achievement[] all = new Achievement[]
    {
        new Achievement("Most wanted", "10 \"tough\" photos", KeyStat.ToughPose, 10),
        new Achievement("Hey, kitty", "10 \"playful\" photos", KeyStat.PlayfulPose, 10),
        new Achievement("I need a nap", "10 \"lazy\" photos", KeyStat.LazyPose, 10),

        new Achievement("Nope!", "Skip 10 photoes", KeyStat.Struggle, 10),

        new Achievement("Likeable", "get 100 likes", KeyStat.Like, 100),
        new Achievement("Follow me", "get 100 followers", KeyStat.Follower, 100),

        new Achievement("Who's boss?", "20 \"tough\" photos", KeyStat.ToughPose, 20),
        new Achievement("...and I know it", "20 \"playful\" photos", KeyStat.PlayfulPose, 20),
        new Achievement("24/7", "20 \"lazy\" photos", KeyStat.LazyPose, 20),

        new Achievement("I like it", "get 30 likes for 1 shot", KeyStat.OneTimeLike, 30),
        new Achievement("+1", "get 30 followers from 1 shot", KeyStat.OneTimeFollower, 30),

        new Achievement("Not my best angle", "Skip 20 photoes", KeyStat.Struggle, 20),

        new Achievement("Awww~", "get 200 likes", KeyStat.Like, 200),
        new Achievement("Have you met Cat?", "get 200 followers", KeyStat.Follower, 200),

        new Achievement("Nice angle", "get 50 likes for 1 shot", KeyStat.OneTimeLike, 50),
        new Achievement("I need this in my feed", "get 50 followers from 1 shot", KeyStat.OneTimeFollower, 50),

        new Achievement("The struggle is real", "Skip 50 photoes", KeyStat.Struggle, 50),

        new Achievement("THIS CAT IS ADORABLE", "get 1000 likes", KeyStat.Like, 1000),
        new Achievement("World's legend", "get 1000 followers", KeyStat.Follower, 1000),

        new Achievement("Never give up", "Skip 100 photoes", KeyStat.Struggle, 100),

        new Achievement("It's going viral", "get 100 likes for 1 shot", KeyStat.OneTimeLike, 100),
        new Achievement("Hey, everyone!", "get 100 followers from 1 shot", KeyStat.OneTimeFollower, 100),

        new Achievement("Yes, this field has the max value", "get " +
                                                         (ProfileWindow.MAX_VALUE + 1).ToString() +
                                                         " likes", KeyStat.Like, ProfileWindow.MAX_VALUE + 1),
        new Achievement("Too easy (beat the game)", "get " +
                                                         (ProfileWindow.MAX_VALUE + 1).ToString() +
                                                         " followers", KeyStat.Follower, ProfileWindow.MAX_VALUE + 1)
    };

    public enum KeyStat
    {
        Like, Follower, OneTimeLike, OneTimeFollower, ToughPose, LazyPose, PlayfulPose, Struggle
    }
    public string name;
    public string text;
    public KeyStat key;
    public int max;
    public int value;

    public Achievement()
    {

    }

    public Achievement(string name, string text, KeyStat key, int max)
    {
        this.name = name;
        this.text = text;
        this.key = key;
        this.max = max;
        value = 0;
    }

    public Achievement(string name, string text, KeyStat key, int max, int value)
    {
        this.name = name;
        this.text = text;
        this.key = key;
        this.max = max;
        this.value = value;
    }
}