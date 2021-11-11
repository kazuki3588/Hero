using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource startSound;
    [SerializeField]
    AudioSource bossSound;
    private void Start()
    {
        startSound.Play();
        bossSound.Stop();
    }
    public void ChangeSound()
    {
        startSound.Stop();
        bossSound.Play();
    }
}
