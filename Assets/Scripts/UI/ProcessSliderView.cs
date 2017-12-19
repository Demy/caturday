using UnityEngine;
using UnityEngine.UI;

public class ProcessSliderView : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        _slider = transform.GetChild(0).GetComponent<Slider>();
        Hide();
    }

    public void UpdateValue(float value)
    {
        Show();
        _slider.value = value;
    }

    public void Show()
    {
        _slider.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _slider.gameObject.SetActive(false);
    }
}