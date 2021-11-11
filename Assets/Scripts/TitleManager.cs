using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
//スタートボタンを管理するクラス
class TitleManager : MonoBehaviour
{
    AudioSource clickSound;
    private void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }
    public void StartBotton()
    {
        clickSound.Play();
        StartCoroutine("SceneLoad");
    }
    IEnumerator SceneLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }
}
