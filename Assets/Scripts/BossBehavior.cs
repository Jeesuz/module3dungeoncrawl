using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossBehavior : MonoBehaviour
{
    GameObject player;
    AIDestinationSetter aiset;
    AIPath aipathing;
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] int currentTarget;
    Rigidbody2D rb;
    Transform gfxChild;
    SpriteRenderer sr;
    CircleCollider2D cc;
    AudioSource audioUse;
    Animator anim;
    public AudioClip tookdamage;
    public AudioClip deathscream;
    public bool bossDead = false;
    PlayerMovement playerMove;
    public int HP = 10;
    float hurtPeriod = 1.5f;
    float hurtTime = 2f;
    public float invulTime = 0f;
    public bool encountered = false;
    float initialDistance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gfxChild = gameObject.transform.GetChild(0);
        cc = GetComponent<CircleCollider2D>();
        sr = gfxChild.GetComponent<SpriteRenderer>();
        aiset = GetComponent<AIDestinationSetter>();
        aipathing = GetComponent<AIPath>();
        audioUse = GetComponent<AudioSource>();
        anim = gfxChild.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMove = player.GetComponent<PlayerMovement>();
        
        initialDistance = aipathing.endReachedDistance;

        Transform points = transform.Find("Patrolpoints");
        GameObject newParent = GameObject.Find("Bosspatrolpoints");
        points.SetParent(newParent.transform);

        for (int i = 0; i < points.childCount; i++)
        {
            patrolPoints.Add(points.GetChild(i));
        }

        SetCurrentTarget(0);

    }

    void SetCurrentTarget(int i) 
    {
        aiset.target = patrolPoints[i % patrolPoints.Count];
        currentTarget = i % patrolPoints.Count;
    }

    public bool OnScreen() 
    {
        float cameraHalfH = Camera.main.orthographicSize;
        float cameraHalfW = cameraHalfH * Camera.main.aspect;
        Vector3 difference = Camera.main.transform.position - transform.position;
        return (Mathf.Abs(difference.x) < cameraHalfW && Mathf.Abs(difference.y) < cameraHalfH);
    }

    public void GetHurt()
    {
        sr.color = Color.red;
        hurtTime = 0f;
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

    public Vector3 DamagePlayer()
    {
        Vector3 pushBack = aipathing.desiredVelocity * 2000f;
        return pushBack;
    }
    
    public void TakeDamage(int damage)
    {
        
        HP = HP - damage;
        if (HP > 0)
        {
            audioUse.PlayOneShot(tookdamage, 0.5f);
        } else if (HP == 0)
        {
            audioUse.PlayOneShot(deathscream, 0.5f);
            anim.Play("bossdeath");
            bossDead = true;
            cc.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HurtUpdate();
        invulTime -= Time.deltaTime;
        if (OnScreen())
        {
            if (HP > 4)
            {
                if (!encountered)
                {
                    invulTime = 1f;
                    encountered = true;
                }
                if (invulTime <= 0.5)
                {
                    aipathing.maxSpeed = 8f;
                } else {
                    aipathing.maxSpeed = 0f;
                }
                Vector3 distance = aiset.target.transform.position - transform.position;
                if (distance.magnitude < aipathing.endReachedDistance)
                {
                    SetCurrentTarget(currentTarget + 1);
                }
            } else if (HP <= 4 && HP > 0) {
                aiset.target = player.transform;

                if (invulTime <= 0.5)
                {
                    aipathing.maxSpeed = 8f;
                } else {
                    aipathing.maxSpeed = 0f;
                }
                if (playerMove.playerBlock)
                {
                    aipathing.endReachedDistance = 5f;
                } else {
                    aipathing.endReachedDistance = initialDistance;
                }
            } else {
                aipathing.maxSpeed = 0f;
            }
            
        }
    }
}
