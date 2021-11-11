using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
//ゲーム全体を管理するクラス
public class GameManager : MonoBehaviour
{
    bool gameClear = false;
    bool gameOver = false;
    [SerializeField]
    Text JugText;
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
    public bool GameClear
    {
        get { return gameClear; }
        set { gameClear = value; }
    }
    private void Update()
    {
        if (gameOver)
        {
            JugText.color = new Color(255,0,0,255);
            JugText.text = "GAME OVER";
            StartCoroutine("BackScene");
        }
        if (gameClear)
        {
            StartCoroutine("GameClearText");
        } 
    }

    IEnumerator BackScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator GameClearText()
    {
        yield return new WaitForSeconds(3f);
        JugText.color = new Color(0, 255, 0, 255);
        JugText.text = "GAMECLEAR";
        StartCoroutine("BackScene");
    }
}
