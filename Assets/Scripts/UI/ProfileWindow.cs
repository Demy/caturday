using UnityEngine;
using UnityEngine.UI;

public class ProfileWindow : MonoBehaviour
{
    public const int MAX_VALUE = 99999;

    public GameObject AchievePrefab;
    public Transform achieves;

    private Text likes;
    private Text followers;
    private Text achievesTitile;
    private Image bestPose;
    private ScrollRect list;

    private bool _isActive;

    void Start()
    {
        bestPose = transform.Find("BestPose").GetComponent<Image>();
        likes = transform.Find("Likes").GetComponent<Text>();
        followers = transform.Find("Followers").GetComponent<Text>();
        achievesTitile = transform.Find("Title").GetComponent<Text>();
        list = transform.Find("Scroll View").GetComponent<ScrollRect>();

        _isActive = true;

        UIUtils.LoadSourceToImage(bestPose, "Poses/" + CatCharacter.bestPose);

        FillAchievements();

        OpenClose();
    }

    private void FillAchievements()
    {
        int complete = 0;
        int count = 0;
        foreach (Achievement achievData in CatCharacter.achievements)
        {
            ++count;
            if (achievData.value >= achievData.max) ++complete;
            GameObject achieve = Instantiate(AchievePrefab);
            achieve.transform.SetParent(achieves.transform);
            achieve.GetComponent<AchievementListView>().Set(achievData);
        }
        achievesTitile.text = "Achievements (" + complete + "/" + count + ")";
    }

    public void OpenClose()
    {
        _isActive = !_isActive;
        transform.Translate(0, _isActive ? 1000f : -1000f, 0);
        if (_isActive) UpdateInfo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OpenClose();
        if (IsActive() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) Scroll(1);
        if (IsActive() && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))) Scroll(-1);
    }

    private void Scroll(int value)
    {
        list.verticalNormalizedPosition += value * 0.01f;
    }

    private void UpdateInfo()
    {
        SetScoreText(likes, CatCharacter.likes);
        SetScoreText(followers, CatCharacter.followers);

        UpdateAchievements();
    }

    private void SetScoreText(Text field, int value)
    {
        field.text = value > MAX_VALUE ? MAX_VALUE + "+" : value.ToString();
    }

    private void UpdateAchievements()
    {
        for (int i = 0; i < CatCharacter.achievements.Length; i++)
        {
            achieves.transform.GetChild(i).GetComponent<AchievementListView>().Set(CatCharacter.achievements[i]);
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }
}