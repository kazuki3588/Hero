using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackButton : MonoBehaviour
{
    [SerializeField]
    AudioSource clickSound;
    public void PushBackButton()
    {
        clickSound.Play();
        StartCoroutine("BackLoadScene");
    }
    IEnumerator BackLoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TitleScene");
    }
}
