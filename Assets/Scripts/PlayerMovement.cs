using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//プレイヤーを管理するクラス
class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    AudioSource attackSoud;
    [SerializeField]
    AudioSource damageVoice;
    [SerializeField]
    AudioSource deathVoice;
    [SerializeField]
    GameObject magic;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    Vector3 offset;
    Animator player_Animator;
    Rigidbody2D rigidbody2d;
    float player_Speed;
    float timeAttack;
    int attackCount = 0;
    int playerHp = 100;
    int max_playerHP = 100;
    [SerializeField]
    Slider hpSlider;
  
    GameManager gameManager;
    CheckGround checkGround;
    int attackPower = 10;
    public int AttackPower
    {
        get { return attackPower; }
    }
    public int PlayerHP
    {
        get { return playerHp; }
        set { playerHp = value; }
    }
    public int Max_PlayerHp
    {
        get { return max_playerHP; }
    }
    enum MOVE_DIRECTION
    {
        STOP,
        LEFT,
        RIGHT,
    }

    void Start()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
        AudioSource[] playerAudios = GetComponents<AudioSource>();
        attackSoud = playerAudios[0];
        damageVoice = playerAudios[1];
        deathVoice = playerAudios[2];
        hpSlider.value = max_playerHP;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        checkGround = GameObject.FindGameObjectWithTag("JugGround").GetComponent<CheckGround>();
    }

    MOVE_DIRECTION moveDirectoin = MOVE_DIRECTION.STOP;

    void Update()
    {
        if (gameManager.GameOver) return;
        timeAttack += Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0) && timeAttack > 0.25)
        {
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                Instantiate(magic, transform.position + offset, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(magic, transform.position + offset, Quaternion.Euler(0, 0, 180));
            }
            attackSoud.Play();
            attackCount++;
            if (attackCount > 3)
                attackCount = 1;
            if (timeAttack > 1f)
                attackCount = 1;
            player_Animator.SetTrigger("Attack" + attackCount);
            timeAttack = 0.0f;
        }

        PlayerMoveSet();
        if (Input.GetKeyDown("space") && checkGround.GroundCheck)
        {
            Jump();
        }
        if (playerHp != 0) return;
        player_Animator.SetInteger("RunInt", 0);
        player_Animator.SetTrigger("Death");
        gameManager.GameOver = true;
    }

    void FixedUpdate()
    {
        PlayerMove();

        var currentPosi = transform.position.x;

        if(currentPosi < -3)
        {
            currentPosi = -3;
        }
        transform.position = new Vector2(currentPosi, transform.position.y);
    }

    void Jump()
    {
        rigidbody2d.AddForce(Vector2.up * jumpPower);
    }

    void PlayerMoveSet()
    {
        float x = Input.GetAxis("Horizontal");
        if (x == 0)
        {
            moveDirectoin = MOVE_DIRECTION.STOP;
            player_Animator.SetInteger("RunInt", 0);
        }
        else if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            moveDirectoin = MOVE_DIRECTION.RIGHT;
            player_Animator.SetInteger("RunInt", 1);
        }
        else if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            moveDirectoin = MOVE_DIRECTION.LEFT;
            player_Animator.SetInteger("RunInt", 1);
        }

       
    }

    void PlayerMove()
    {
        switch (moveDirectoin)
        {
            case MOVE_DIRECTION.STOP:
                player_Speed = 0;
                break;
            case MOVE_DIRECTION.LEFT:
                player_Speed = -4;
                break;
            case MOVE_DIRECTION.RIGHT:
                player_Speed = 4;
                break;
        }

        rigidbody2d.velocity = new Vector2(player_Speed, rigidbody2d.velocity.y);
 
    }

    void SetPlayerHP()
    {
        hpSlider.value = playerHp;
    }

    void CalculationPlayerHP(float damage)
    {
        playerHp = (int)Mathf.Clamp(playerHp - damage, 0, max_playerHP);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy")
        {
            CalculationPlayerHP(10);
            player_Animator.SetTrigger("Damege");
            hpSlider.value = playerHp;
        }
        if (collision.gameObject.tag == "Instant death")
        {
            playerHp = 0;
            hpSlider.value = playerHp;
        }
  
    }   
    public IEnumerator DeathPlayer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void Damage()
    {
        if (gameManager.GameOver) return;
        CalculationPlayerHP(3);
        player_Animator.SetTrigger("Damege");
        hpSlider.value = playerHp;
    }
    public void DamageAnimation()
    {
        player_Animator.SetTrigger("Damege");
    }
    public void SetHP()
    {
        hpSlider.value = playerHp;
    }
    public void DamageVoice()
    {
        damageVoice.PlayOneShot(damageVoice.clip);
    }
    public void DeathVoice()
    {
        deathVoice.PlayOneShot(deathVoice.clip);
    }
}
