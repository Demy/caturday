using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PoseKeySquenceView : PoseImage
{
    private Text keyText;
    private PoseStatus _poseStatus;

    private string currentKey = "";

    public void SetPose(PoseStatus poseStatus)
    {
        _poseStatus = poseStatus;
        base.SetPose(poseStatus.pose);
        keyText = transform.Find("Key").GetComponent<Text>();
        keyText.text = _poseStatus.GetCurrentKey();

        EventManager.StartListening(PoseStatus.KEY_CHANGED, ShowCurrentKey);
    }

    private void ShowCurrentKey()
    {
        string newKey = _poseStatus.GetCurrentKey();
        if (newKey == "")
        {
            keyText.text = "";
        }
        else if (keyText.text != newKey)
        {
            keyText.text = _poseStatus.GetCurrentKey();
            GetComponent<RectTransform>().DORotate(new Vector3(0, 180f, 0), 0.2f).SetRelative().
                SetLoops(2, LoopType.Incremental);
        }
    }

    void OnDestroy()
    {
        EventManager.StopListening(PoseStatus.KEY_CHANGED, ShowCurrentKey);
    }
}