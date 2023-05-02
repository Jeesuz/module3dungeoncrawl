using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SouthDoorBehavior : MonoBehaviour
{
    AudioSource audioUse;
    public AudioClip open;
    PlayerMovement playerCharMove;
    GameObject playerChar;
    // Start is called before the first frame update
    void Start()
    {
        playerChar = GameObject.FindGameObjectWithTag("Player");
        playerCharMove = playerChar.GetComponent<PlayerMovement>();
        audioUse = GetComponent<AudioSource>();
    }
    void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerCharMove.silverKeys > 0)
            {
                audioUse.PlayOneShot(open, 0.7f);
                playerCharMove.silverKeys--;
                gameObject.GetComponent<TilemapRenderer>().enabled = false;
                gameObject.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
