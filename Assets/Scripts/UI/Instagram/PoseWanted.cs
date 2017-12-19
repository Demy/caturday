using UnityEngine;
using UnityEngine.UI;

public class PoseWanted : PoseImage
{
    private const string GOOD_MOOD = "Awesome mood for this shot!";
    private const string NO_MOOD = "You feel kinda OK";
    private const string BAD_MOOD = "Your mood doesn't match the shot";

    public Color goodColor;
    public Color okColor;
    public Color badColor;

    void Start()
    {
    }

    public void SetEfficiency(int efficiency)
    {
        Text warning = transform.parent.Find("Warning").GetChild(0).GetComponent<Text>();
        if (efficiency > 0) SetWarning(warning, GOOD_MOOD, goodColor);
        if (efficiency == 0) SetWarning(warning, NO_MOOD, okColor);
        if (efficiency < 0) SetWarning(warning, BAD_MOOD, badColor);
    }

    private void SetWarning(Text warning, string text, Color color)
    {
        warning.color = color;
        warning.text = text;
    }
}