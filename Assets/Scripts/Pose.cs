using System;

[Serializable]
public class Pose
{
    public static Pose Playful = new Pose("Playful", "IdlePlayful", Achievement.KeyStat.PlayfulPose,
        new string[]{"Q", "W"},
        new Attribute.Type[]{Attribute.Type.Playfullness, Attribute.Type.Curiosity},
        new Attribute.Type[]{Attribute.Type.Strength, Attribute.Type.Sociality});
    public static Pose Toughguy = new Pose("Fight", "IdleFight", Achievement.KeyStat.ToughPose,
        new string[]{"Q", "W"},
        new Attribute.Type[]{Attribute.Type.Toughness, Attribute.Type.Strength},
        new Attribute.Type[]{Attribute.Type.Lazyness, Attribute.Type.Playfullness});
    public static Pose Lazy = new Pose("Lazy", "IdleLazy", Achievement.KeyStat.LazyPose,
        new string[]{"Q", "W"},
        new Attribute.Type[]{Attribute.Type.Lazyness, Attribute.Type.Sociality},
        new Attribute.Type[]{Attribute.Type.Curiosity, Attribute.Type.Toughness});

    public static Pose Struggle = new Pose("Struggle", "none", Achievement.KeyStat.Struggle,
        new string[]{"E", "A", "S", "D"},
        new Attribute.Type[]{},
        new Attribute.Type[]{},
        true);

    public static Pose[] poses = new[] {Playful, Toughguy, Lazy};

    public string icon;
    public string idleName;
    public Achievement.KeyStat achieveKey;
    public string[] keys;
    public bool isSequence = false;
    public Attribute.Type[] positiveAttributes;
    public Attribute.Type[] negativeAttributes;

    public Pose(string icon, string idleName, Achievement.KeyStat achieveKey, string[] keys,
        Attribute.Type[] positiveAttributes, Attribute.Type[] negativeAttributes,
        bool isSequence = false)
    {
        this.icon = icon;
        this.idleName = idleName;
        this.achieveKey = achieveKey;
        this.keys = keys;
        this.positiveAttributes = positiveAttributes;
        this.negativeAttributes = negativeAttributes;
        this.isSequence = isSequence;
    }
}