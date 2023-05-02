using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    GameObject target;
    SpriteRenderer sr;
    Rigidbody2D rb;
    AudioSource audioUse;
    public AudioClip enemydeath;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("AttackPoint");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioUse = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();
            if (enemy.invulTime <= 0)
            {
                enemy.TakeDamage(1);
                if (enemy.HP > 0)
                {
                    enemy.GetHurt();
                }
                enemy.invulTime = 0.3f;
            }
        } else if (other.gameObject.tag == "Boss")
        {
            BossBehavior boss = other.GetComponent<BossBehavior>();
            if (boss.invulTime <= 0)
            {
                boss.TakeDamage(1);
                if (boss.HP > 0)
                {
                    boss.GetHurt();
                }
                boss.invulTime = 1.5f;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        if (target.transform.localPosition == new Vector3(-0.75f, 0, 0))
        {
            sr.flipX = true;
        } else {
            sr.flipX = false;
        }
    }
}
