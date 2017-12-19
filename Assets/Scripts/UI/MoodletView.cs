using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoodletView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MoodletEffect item;

    private UIHint hint;

    void Start()
    {
    }

    public void SetItem(MoodletEffect item)
    {
        this.item = item;

        UIUtils.LoadSourceToImage(GetComponent<Image>(), "Moodlets/" + item.icon);
    }

    public void SetHint(UIHint hint)
    {
        this.hint = hint;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hint != null)
        {
            hint.Show(item.name, GetTextFor(item));
            hint.transform.Translate(Input.mousePosition - hint.transform.position);
        }
    }

    private string GetTextFor(MoodletEffect item)
    {
        return item.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hint != null) hint.Hide();
    }
}