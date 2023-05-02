using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FastEnemyMovement : MonoBehaviour
{
    AIDestinationSetter aiset;
    AIPath aipathing;
    GameObject Player;
    GameObject spawnPoint;
    GameObject spawnPointContainer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform gfxChild;
    public int HP = 3;
    float hurtPeriod = 0.3f;
    float hurtTime = 1f;
    float invulTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gfxChild = gameObject.transform.GetChild(0);
        sr = gfxChild.GetComponent<SpriteRenderer>();
        aiset = GetComponent<AIDestinationSetter>();
        aipathing = GetComponent<AIPath>();
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnPointContainer = GameObject.Find("EnemySpawnPoints");

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

    public Vector3 DamagePlayer()
    {
        Vector3 pushBack = aipathing.desiredVelocity * 1500f;
        return pushBack;
    }
    public void TakeDamage(int damage)
    {
        HP = HP - damage;
        rb.AddForce(-(aipathing.desiredVelocity) * 1500f);
    }
    // Update is called once per frame
    void Update()
    {
        HurtUpdate();
        if (OnScreen())
        {
            aiset.target = Player.transform;
        } else {
            aiset.target = spawnPoint.transform;
            gameObject.transform.position = spawnPoint.transform.position;
        }
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }
}
