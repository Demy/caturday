using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HomeRoom : MonoBehaviour
{
    private const float RUNNING_MOODLET_TIME = 5f;

    [SerializeField]
    private JobBubble bubble;

    private Interactable[] objects;
    private List<Interactable> inactive = new List<Interactable>();

    private float timeLeft = 15f;

    [SerializeField]
    private ProfileWindow pauseMenu;
    [SerializeField]
    private CatMovement cat;
    private KeyboardController conrtoller;

    void Start()
    {
        conrtoller = cat.gameObject.GetComponent<KeyboardController>();

        CatCharacter.hasActiveJob = false;

        objects = FindObjectsOfType<Interactable>();

        timeLeft = CatCharacter.followers > 50 ? 12f : CatCharacter.followers == 0 ? 20f : 15f;

        if (CatCharacter.followers > 10)
            DeactivateRandomObjects();
    }

    void FixedUpdate()
    {
        if (!pauseMenu.IsActive())
        {
            timeLeft -= Time.deltaTime;
            if (!conrtoller.IsActive())
            {
                conrtoller.SetActive(true);
                cat.IsPaused(false);
            }
        }
        else
        {
            if (conrtoller.IsActive())
            {
                conrtoller.SetActive(false);
                cat.IsPaused(true);
            }
        }
        if (cat.GetTimeRunning() >= RUNNING_MOODLET_TIME)
        {
            CatCharacter.AddMoodlet(MoodletEffect.Running);
        }
        if (timeLeft <= 0) InitQuest();
    }

    private void DeactivateRandomObjects()
    {
        for (int i = 0; i < 3; i++)
        {
            objects[Random.Range(0, objects.Length)].SetUsable(false);
        }
    }

    void InitQuest()
    {
        CatCharacter.currentQuest = CatCharacter.Jobs.Instagram;
        bubble.Show(CatCharacter.currentQuest);
        CatCharacter.hasActiveJob = true;

        conrtoller.SetActive(false);
        cat.NavigateToExit();

        Invoke("StartJob", 2.5f);
    }

    void StartJob()
    {
        if (CatCharacter.hasActiveJob)
            SceneManager.LoadScene(CatCharacter.currentQuest.ToString() + "Scene");
    }
}