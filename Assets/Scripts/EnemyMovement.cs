using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    AIDestinationSetter aiset;
    AIPath aipathing;
    GameObject Player;
    GameObject spawnPoint;
    GameObject spawnPointContainer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    CircleCollider2D cc;
    Transform gfxChild;
    PlayerMovement playerMove;
    AudioSource audioUse;
    public AudioClip tookdamage;
    public AudioClip deathscream;
    public int HP = 3;
    float hurtPeriod = 0.3f;
    float hurtTime = 1f;
    public float invulTime = 0f;
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
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnPointContainer = GameObject.Find("EnemySpawnPoints");
        playerMove = Player.GetComponent<PlayerMovement>();
        initialDistance = aipathing.endReachedDistance;

        spawnPoint = Instantiate(new GameObject(), 
        transform.position, transform.rotation, spawnPointContainer.transform);
    }

    bool OnScreen() 
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

    void KillSelf()
    {
        cc.enabled = false;
        sr.enabled = false;
        Destroy(gameObject, 0.5f);
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
            rb.AddForce(-(aipathing.desiredVelocity) * 2000f);
        } else if (HP == 0)
        {
            audioUse.PlayOneShot(deathscream, 0.5f);
            KillSelf();
        }
    }
    // Update is called once per frame
    void Update()
    {
        HurtUpdate();
        invulTime -= Time.deltaTime;
        if (OnScreen() && Player != null)
        {
            aiset.target = Player.transform;
        } else {
            aiset.target = spawnPoint.transform;
            gameObject.transform.position = spawnPoint.transform.position;
            HP = 3;
        }

        if (playerMove.playerBlock)
        {
            aipathing.endReachedDistance = 5f;
        } else {
            aipathing.endReachedDistance = initialDistance;
        }
    }
}
