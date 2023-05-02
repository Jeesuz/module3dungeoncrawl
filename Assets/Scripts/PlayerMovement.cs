using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public int HP = 10;
    float invulTime = 0f;
    Rigidbody2D rb;
    CircleCollider2D cc;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioUse;
    public AudioClip swordattack;
    public AudioClip keypickup;
    public AudioClip tookdamage;
    public AudioClip crossuse;
    Vector3 initialPosition;
    public int silverKeys = 0;
    public bool bossKey;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        audioUse = GetComponent<AudioSource>();
        initialPosition = attackPoint.transform.position;
    }

    Vector2 displacement;
    float walkSpeed = 300f;
    [SerializeField] GameObject attack;
    [SerializeField] GameObject attackPoint;
    float attackTime = 0f;
    float attackTimeInterval = 0.5f;
    bool playerAttack;
    public bool playerBlock;
    public bool crossGet = false;
    float hurtTime = 2f;
    float hurtPeriod = 1.5f;

    public void GetHurt()
    {
        sr.color = Color.red;
        hurtTime = 0f;
        audioUse.PlayOneShot(tookdamage, 0.5f);
    }

    void HurtUpdate()
    {
        if (hurtTime < hurtPeriod)
        {
            sr.color = Color.Lerp(Color.red, Color.black, hurtTime / hurtPeriod);
        } else {
            sr.color = Color.white;
        }
        hurtTime += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy" && invulTime <= 0f && !playerBlock)
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            rb.AddForce(enemy.DamagePlayer());
            GetHurt();
            HP--;
            invulTime = 1.5f;
        } else if (other.gameObject.tag == "Boss" && invulTime <= 0f && !playerBlock)
        {
            BossBehavior boss = other.gameObject.GetComponent<BossBehavior>();
            rb.AddForce(boss.DamagePlayer());
            GetHurt();
            HP--;
            invulTime = 1.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Key")
        {
            audioUse.PlayOneShot(keypickup, 0.3f);
        }
        if (other.gameObject.tag == "Exit")
        {
            Application.Quit();
        }
    }

    void PlayerAttack()
    {
            GameObject newAttack = Instantiate(
            attack, attackPoint.transform.position, attackPoint.transform.rotation);
            attackTime = attackTimeInterval;
            Destroy(newAttack, 0.2f);
            audioUse.PlayOneShot(swordattack, 0.2f);
    }

    void Update() 
    {
        HurtUpdate();
        playerAttack = Input.GetButton("Fire1");
        if (crossGet)
        {
            playerBlock = Input.GetButton("Fire2");
        }

        if (playerAttack && attackTime <= 0 && !playerBlock)
        {
            PlayerAttack();
        }
        attackTime -= Time.deltaTime;
        invulTime -= Time.deltaTime;

        //if (invulTime <= 0)
        //{
        //    cc.enabled = true;
        //} else {
        //    cc.enabled = false;
        //}

        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        displacement = new Vector2(
            Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        displacement = Vector2.ClampMagnitude(displacement, 1);
        rb.velocity = displacement * Time.deltaTime * walkSpeed;

        if (playerBlock)
        {
            animator.Play("player_cross");
            if (!audioUse.isPlaying)
            {
                audioUse.PlayOneShot(crossuse, 0.5f);
            }
        }
         else if (rb.velocity.magnitude > 0)
        {
            animator.Play("player_walk");
            if (displacement.x < 0)
            {
                sr.flipX = true;
                attackPoint.transform.localPosition = new Vector3(-0.75f, 0 , 0);
            } else if (displacement.x > 0)
            {
                sr.flipX = false;
                attackPoint.transform.localPosition = new Vector3(0.75f, 0, 0);
            }
        } else {
            animator.Play("player_idle");
        }
    }
}
