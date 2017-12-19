using UnityEngine;

public class InstagramSounds : MonoBehaviour
{
    void Start()
    {

    }

    public void StopBG()
    {
        transform.Find("BackSound").gameObject.SetActive(false);
    }

    public void PlayLose()
    {
        PlaySound("ScratchSound");
    }

    public void PlayCamera()
    {
        PlaySound("CameraClick");
    }

    public void PlayWin()
    {
        PlaySound("WinSound");
    }

    public void PlaySound(string soundName)
    {
        transform.Find(soundName).GetComponent<AudioSource>().Play();
    }
}