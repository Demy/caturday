using UnityEngine;

public class Window : MonoBehaviour
{
    protected bool isShown = true;

    public virtual void Show()
    {
        isShown = true;
        if (transform.position.y < 500f) transform.Translate(0, 1000f, 0);
    }

    public virtual void Hide()
    {
        isShown = false;
        if (transform.position.y > -500f) transform.Translate(0, -1000f, 0);
    }

    void Update()
    {
        if (isShown && (Input.GetKeyDown(KeyCode.Return) ||
                        Input.GetKeyDown(KeyCode.Escape) ||
                        Input.GetKeyDown(KeyCode.X)))
            Hide();
    }
}