using UnityEngine;
using UnityEngine.UI;

public class PoseImage : MonoBehaviour
{
    protected Pose pose;

    public virtual void SetPose(Pose pose)
    {
        this.pose = pose;

        UIUtils.LoadSourceToImage(GetComponent<Image>(), "Poses/" + pose.icon + "Try");
    }
}