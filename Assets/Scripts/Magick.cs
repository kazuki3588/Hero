using UnityEngine;
    
//魔法を管理するクラス
class Magick : MonoBehaviour
{
    [SerializeField]
    float magickSpeed;
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * magickSpeed;
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<MouseEnemy>().MouseDestroyEnemy();
        }
    }
}
