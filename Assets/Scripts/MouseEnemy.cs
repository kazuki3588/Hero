using UnityEngine;

//MouseEnemyの移動を管理するクラス
public class MouseEnemy : MonoBehaviour
{
    [SerializeField]
    LayerMask GroundLayer;
    [SerializeField]
    GameObject deathEffect;
    Rigidbody2D rigidbody2d;
    float speed = 0;

    enum MOVE_DIRECTION
    {
        STOP,
        LEFT,
        RIGHT,
    }
    MOVE_DIRECTION moveDirection = MOVE_DIRECTION.LEFT;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!IsGround())
        {
            ChangeDirection();
        }
    }
    private void FixedUpdate()
    {
        switch (moveDirection)
        {
            case MOVE_DIRECTION.STOP:
                speed = 0;
                break;
            case MOVE_DIRECTION.LEFT:
                transform.localScale = new Vector3(1, 1, 1);
                speed = -3;
                break;
            case MOVE_DIRECTION.RIGHT:
                transform.localScale = new Vector3(-1, 1, 1);
                speed = 3;
                break;
        }
        rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
    }

    bool IsGround()
    {
        Vector3 startVec = transform.position + transform.right * -0.5f * transform.localScale.x;
        Vector3 endVec = startVec - transform.up * 0.5f;
        Debug.DrawLine(startVec, endVec);   
        return Physics2D.Linecast(startVec, endVec, GroundLayer);
    }

    void ChangeDirection()
    {
        if(moveDirection == MOVE_DIRECTION.LEFT)
        {
            moveDirection = MOVE_DIRECTION.RIGHT;
        }
        else
        {
            moveDirection = MOVE_DIRECTION.LEFT;
        }
    }
    public void MouseDestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
