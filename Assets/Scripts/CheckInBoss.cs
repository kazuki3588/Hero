using UnityEngine;

public class CheckInBoss : MonoBehaviour
{
    [SerializeField]
    GameObject bossBlock;
    [SerializeField]
    GameObject bossUI;
    bool checkFlag;
    bool blockSetFlag;
    SoundManager soundManager;
    public bool CheckFlag
    {
        get { return checkFlag; }
    }
    private void Start()
    {
        soundManager = GameObject.Find("BGM").GetComponent<SoundManager>();
    }
    void Update()
    {
        if (blockSetFlag) return;
        if (!checkFlag) return;
        bossUI.SetActive(true);
        Instantiate(bossBlock, new Vector3(35.5f, 4.5f, 0), Quaternion.identity);
        Instantiate(bossBlock, new Vector3(35.5f, 3.5f, 0), Quaternion.identity);
        soundManager.ChangeSound();
        GetComponent<AudioSource>().Play();
        blockSetFlag = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkFlag) return;
        if(collision.gameObject.tag == "Player")
        {
            checkFlag = true;
        }


    }
}
