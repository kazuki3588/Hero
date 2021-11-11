using UnityEngine;

public class BossMagic : MonoBehaviour
{
    [SerializeField]
    float magickSpeed;
    PlayerMovement player;
    BossEnemy bossInst;
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * magickSpeed;
        player = GameObject.Find("Hero").GetComponent<PlayerMovement>();
        Destroy(gameObject, 10f);
        bossInst = GameObject.Find("BossEnemy").GetComponent<BossEnemy>();
    }
    private void Update()
    {
        if (bossInst.DeathFlag)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            player.Damage();
            Destroy(gameObject);
        }

    }
}
