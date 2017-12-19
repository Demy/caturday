using UnityEngine;
using UnityEngine.UI;

public class UIUtils
{
    public static void PlaceUIObjectNear(GameObject obj, GameObject target)
    {
        Vector3 charPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        RectTransform transform = obj.GetComponent<RectTransform>();
        transform.Translate(charPosition - transform.position);
    }

    public static void LoadSourceToImage(Image image, string source)
    {
        var sprite = Resources.Load<Sprite>(source);
        if (sprite != null) image.sprite = sprite;
    }
}