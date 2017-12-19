using UnityEngine;
using UnityEngine.UI;

public class AchievementListView : MonoBehaviour
{
    public void Set(Achievement data)
    {
        transform.Find("Label").GetComponent<Text>().text = data.name;
        transform.Find("Description").GetComponent<Text>().text = data.text;
        Slider progress = transform.Find("Progress").GetComponent<Slider>();
        progress.maxValue = data.max;
        progress.value = data.value;
    }
}