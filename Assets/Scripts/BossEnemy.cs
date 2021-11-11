using UnityEngine;
using System.Collections;
using UnityEngine.UI;
class BossEnemy : MonoBehaviour
{
    enum ENEMY_STATE
    {
        IDLE,
        WALKING,
        ATTACK,
        DEATH,
    }
    [SerializeField]
    AudioSource damageVoice;
    [SerializeField]
    AudioSource deathVoice;
    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    float attackRadius;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    GameObject bossMagic;
    [SerializeField]
    Vector3 magicOffset;
    [SerializeField]
    Slider bossSlider;
    ENEMY_STATE enemy_State = ENEMY_STATE.IDLE;
    CheckInBoss checkInInst;
    GameObject target;
    Rigidbody2D rigidbody2d;
    Animator bossEnemyAnimator;
    float destinationX;
    [SerializeField]
    float stoppingDistance;
    float velocity = 0;
    bool arrived = false;
    bool firstFlag = false;
    [SerializeField]
    float walkSpeed;
    int bossHp = 1000;
    int max_BossHP = 1000;
    GameManager gameManagerInst;
    public bool DeathFlag
    {
        get;set;
    }
    private void Start()
    {
        AudioSource[] bossEmenyAudios = GetComponents<AudioSource>();
        damageVoice = bossEmenyAudios[0];
        deathVoice = bossEmenyAudios[1];
        destinationX = transform.position.x;
        checkInInst = GameObject.FindGameObjectWithTag("CheckIn").GetComponent<CheckInBoss>();
        target = GameObject.Find("Hero");
        rigidbody2d = GetComponent<Rigidbody2D>();
        bossEnemyAnimator = GetComponent<Animator>();
        gameManagerInst = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossSlider.value = max_BossHP;
    }
    void Update()
    {
        if (gameManagerInst.GameClear) return;  
        float direction = destinationX - transform.position.x;
        float distance = Mathf.Abs(transform.position.x - destinationX);
        if (direction > 0)
        {
            transform.localScale= new Vector3(-1, 1, 1);//向きを反転
            
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
       if (arrived || distance < stoppingDistance)
            arrived = true;
        velocity = direction * walkSpeed;
        if (arrived)
        {
            velocity = 0;
            bossEnemyAnimator.SetBool("bossEnemyWalk", false);
        }
           
        else
            bossEnemyAnimator.SetBool("bossEnemyWalk", true);
        rigidbody2d.velocity = new Vector2(velocity,rigidbody2d.velocity.y);

        switch (enemy_State)
        {
            case ENEMY_STATE.IDLE:
                if (!checkInInst.CheckFlag) return;
                    
                enemy_State = ENEMY_STATE.WALKING;
            break;
            case ENEMY_STATE.WALKING:
                Walking();
                if (distance < stoppingDistance && firstFlag)
                    enemy_State = ENEMY_STATE.ATTACK;
                    firstFlag = true;
            
                break;
            case ENEMY_STATE.ATTACK:
                Attack();
                enemy_State = ENEMY_STATE.WALKING;
                break;
        }
        if (bossHp != 0) return;
        bossEnemyAnimator.SetBool("bossEnemyWalk", false);
        bossEnemyAnimator.SetTrigger("bossEnemyDeath");
        gameManagerInst.GameClear = true;
    }
    void Walking()
    {
        if (gameManagerInst.GameOver) return;
       arrived = false;
       destinationX = target.transform.position.x;
    }

    void Attack()
    {
        if (gameManagerInst.GameOver) return;
        bossEnemyAnimator.SetTrigger("bossEnemyAttack");
    }
    public void HitAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
        foreach (Collider2D hitPlayer in hitPlayers)
        {
            hitPlayer.GetComponent<PlayerMovement>().Damage();
        }
    }
    void SetPlayerHP()
    {
        bossSlider.value = bossHp;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
    void CalculationBossHP(float damage)
    {
        bossHp = (int)Mathf.Clamp(bossHp - damage, 0, max_BossHP);
    }
    public IEnumerator DeathBossMonster()
    {
        DeathFlag = true;   
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Magic")
        {
            DamageVoice();
            CalculationBossHP(target.GetComponent<PlayerMovement>().AttackPower);
            bossSlider.value = bossHp;
        }
    }
    void DamageVoice()
    {
        damageVoice.PlayOneShot(damageVoice.clip);
    }
    public void DeathVoice()
    {
        deathVoice.PlayOneShot(deathVoice.clip);
    }
    public void BossMagicAttack()
    {
        if (transform.localScale == new Vector3(1,1,1))
        {
            Instantiate(bossMagic, transform.position + magicOffset, Quaternion.Euler(0,0,180));
        }
        else if (transform.localScale == new Vector3(-1,1,1))
        {
            Instantiate(bossMagic, transform.position + magicOffset, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            return;
        }
    }
}
