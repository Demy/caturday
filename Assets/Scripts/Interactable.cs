using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public const string PROCESS = "Process";

    private const float PROCESS_STEP = 0.2f;
    private const float PROCESS_DECREASION_STEP = 0.005f;

    public MoodletEffect effect;
    public GameObject activeSkin;
    public GameObject inactiveSkin;

    private bool _isUsable = true;

    private Animator anim;
    private float process = 0;
    [SerializeField]
    private ProcessSliderView indicator;

    void Start()
    {
        anim = GetComponent<Animator>();
        UpdateSkins();
    }

    void Update()
    {
        if (process > 0)
        {
            process = Math.Max(0, process - PROCESS_DECREASION_STEP);
            indicator.UpdateValue(process);

            if (process == 0) indicator.Hide();
        }
    }

    public void Interact(GameObject cat)
    {
        if (!_isUsable) return;
        if (anim != null) anim.SetTrigger("Interract");
        process += PROCESS_STEP;
        if (process >= 1)
        {
            ActivateEffect(cat);
            process = PROCESS_DECREASION_STEP;
            indicator.UpdateValue(1);
            SetUsable(false);
        }
        else
        {
            UIUtils.PlaceUIObjectNear(indicator.gameObject, cat.gameObject);
        }
    }

    public bool IsUsable()
    {
        return _isUsable;
    }

    public void SetUsable(bool value)
    {
        _isUsable = value;
        UpdateSkins();
        if (anim != null) anim.SetBool("Active", value);
    }

    private void UpdateSkins()
    {
        if (inactiveSkin != null)
        {
            activeSkin.SetActive(_isUsable);
            inactiveSkin.SetActive(!_isUsable);
        }
    }

    private void ActivateEffect(GameObject cat)
    {
        CatCharacter.AddMoodlet(effect);
    }
}