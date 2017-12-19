using UnityEngine;

public class MoodletsView : MonoBehaviour
{
    public UIHint hint;
    public GameObject MoodletPrefab;

    void Start()
    {
        EventManager.StartListening(MoodletEffect.EFFECT_ADDED, RefreshMoodlets);
        EventManager.StartListening(MoodletEffect.EFFECT_REMOVED, RefreshMoodlets);
        RefreshMoodlets();
    }

    void RefreshMoodlets()
    {
        RemoveAll();
        foreach (MoodletEffect effect in CatCharacter.effects)
        {
            AddIconFor(effect);
        }
    }

    private void RemoveAll()
    {
        int i = transform.childCount;
        while (i-- > 0)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void AddIconFor(MoodletEffect effect)
    {
        GameObject newMoodlet = Instantiate(MoodletPrefab);
        newMoodlet.GetComponent<MoodletView>().SetItem(effect);
        newMoodlet.GetComponent<MoodletView>().SetHint(hint);
        newMoodlet.transform.SetParent(transform);
    }

    void OnDestroy()
    {
        EventManager.StopListening(MoodletEffect.EFFECT_ADDED, RefreshMoodlets);
        EventManager.StopListening(MoodletEffect.EFFECT_REMOVED, RefreshMoodlets);
    }
}