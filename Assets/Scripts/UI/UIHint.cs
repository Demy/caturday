using UnityEngine;
using UnityEngine.UI;

public class UIHint : MonoBehaviour
{
    private Text title;
    private Text content;

    void Start()
    {
        title = transform.Find("Title").GetComponent<Text>();
        content = transform.Find("Text").GetComponent<Text>();
        Hide();
    }

    public void Show(string titleText, string contentText)
    {
        title.text = titleText;
        content.text = contentText;
    }

    public void Hide()
    {
        transform.Translate(0, -1000f, 0);
    }
}