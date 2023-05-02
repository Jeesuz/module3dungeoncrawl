using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDoorBehavior : MonoBehaviour
{
    AudioSource audioUse;
    public AudioClip open;
    public AudioClip close;
    PlayerMovement playerCharMove;
    GameObject playerChar;
    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerChar = GameObject.FindGameObjectWithTag("Player");
        playerCharMove = playerChar.GetComponent<PlayerMovement>();
        playerRb = playerChar.GetComponent<Rigidbody2D>();
        audioUse = GetComponent<AudioSource>();
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerCharMove.bossKey == true)
            {
                audioUse.PlayOneShot(open, 0.7f);
                Time.timeScale = 0;
                playerRb.AddForce(transform.up * 15f * Time.unscaledTime);
                gameObject.GetComponent<TilemapRenderer>().enabled = false;
                gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            if (playerCharMove.bossKey == true)
            {
                playerRb.AddForce(transform.up * 15f * Time.unscaledTime);
                gameObject.GetComponent<TilemapRenderer>().enabled = false;
                gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        gameObject.GetComponent<TilemapRenderer>().enabled = true;
        gameObject.GetComponent<TilemapCollider2D>().isTrigger = false;
        if (playerCharMove.bossKey == true)
        {
            audioUse.PlayOneShot(close, 0.7f);
            playerCharMove.bossKey = false;
            Time.timeScale = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
