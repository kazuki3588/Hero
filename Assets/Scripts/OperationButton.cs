using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
class OperationButton : MonoBehaviour
{
    [SerializeField]
    AudioSource clickSound;
    public void PushButtonOperation()
    {
        clickSound.Play();
        StartCoroutine("LoadOperationScene");
    }
    IEnumerator LoadOperationScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("OperationScene");
    }
}
