using System;
using System.Collections.Generic;

[Serializable]
public class MoodletEffect
{
    public const string EFFECT_ADDED = "Effect added";
    public const string EFFECT_REMOVED = "Effect removed";

    public static MoodletEffect Running = new MoodletEffect("Running", "Walking",
        "(for running for 5 sec or more) \nI'll attack this shadow! And will relocate towards the sofa.",
        new Attribute(Attribute.Type.Playfullness, 1),
        new Attribute(Attribute.Type.Lazyness, -1));

    public string name = "";
    public string icon = "";
    public string text = "";
    public List<Attribute> effects = new List<Attribute>();

    public MoodletEffect()
    {

    }

    public MoodletEffect(string name, string icon, string text)
    {
        this.name = name;
        this.icon = icon;
        this.text = text;
    }

    public MoodletEffect(string name, string icon, string text, List<Attribute> effects)
    {
        this.name = name;
        this.icon = icon;
        this.text = text;
        this.effects = effects;
    }

    public MoodletEffect(string name, string icon, string text, Attribute positive)
    {
        this.name = name;
        this.icon = icon;
        this.text = text;
        effects.Add(positive);
    }

    public MoodletEffect(string name, string icon, string text, Attribute positive, Attribute negative)
    {
        this.name = name;
        this.icon = icon;
        this.text = text;
        if (positive != null) effects.Add(positive);
        if (positive != null) effects.Add(negative);
    }
}