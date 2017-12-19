using UnityEngine;
using UnityEngine.UI;

public class JobBubble : MonoBehaviour
{
    private GameObject panel;
    private Image icon;
    public GameObject exitArrow;

    void Start()
    {
        panel = transform.GetChild(0).gameObject;
        icon = panel.transform.GetChild(0).GetComponent<Image>();
        Hide();
    }

    public void Show(CatCharacter.Jobs job)
    {
        panel.SetActive(true);
        exitArrow.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
        exitArrow.SetActive(false);
    }
}